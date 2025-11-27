using System;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Web.UI;


public partial class Franchise : System.Web.UI.Page
{
    private string adminEmail = string.Empty;
    private string key = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        email();
        if (!IsPostBack)
        {
            string siteKey = captchaDetails("captchaSiteKey");
            ltrReCaptcha.Text = string.Format("<div class='g-recaptcha' data-sitekey='{0}'></div>", siteKey);
        }
    }
    private void email()
    {
        using (SqlConnection cn = new SqlConnection(Program.Connection))
        {
            try
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select [data] from [mastercodes] where [Options]='adminEmail'", cn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            adminEmail = reader[0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
    }
    private bool VerifyReCaptcha(string response)
    {
        string secretKey = captchaDetails("captchaSecratKey");
        using (var client = new System.Net.WebClient())
        {
            var result = client.DownloadString(
                string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            return result.Contains("\"success\": true");
        }
    }

    private string captchaDetails(string response)
    {
        using (SqlConnection cn = new SqlConnection(Program.Connection))
        {
            try
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select [data] from [mastercodes] where [Options]='" + response + "'", cn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            key = reader[0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
        return key;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtAnualIncome.SelectedIndex == 0)
        {
            lblError.Text = "Please select your annual income.";
            return;
        }
        // Get form values
        string fullName = txtFullName.Text.Trim();
        string age = txtAge.Text.Trim();
        string phone = txtPhone.Text.Trim();
        string email = txtEmail.Text.Trim();
        string occupation = txtOccupation.Text.Trim();
        string anualIncome = txtAnualIncome.SelectedValue;
        string address = txtAddress.Text.Trim();
        string city = txtCity.Text.Trim();
        string state = txtState.Text.Trim();
        string pincode = txtPincode.Text.Trim();
        string loan = txtLoan.Text.Trim();
        string loanAmount = txtLoanAmount.Text.Trim();
        string cibil = txtCible.Text.Trim();
        string businessExp = ddlBusinessExp.SelectedValue;
        string businessType = txtBusinessType.Text.Trim();
        string location = txtLocation.Text.Trim();
        string ownProperty = ddlOwnProperty.SelectedValue;
        string leasePlan = ddlLeasePlan.SelectedValue;
        string propertySize = txtPropertySize.Text.Trim();
        string budget = ddlBudget.SelectedValue;
        string comments = txtComments.Text.Trim();


        using (SqlConnection cn = new SqlConnection(Program.Connection))
        {
            try
            {
                cn.Open();
                string query = @"INSERT INTO FranchiseInquiry 
                (FullName, Age, PhoneNumber, EmailAddress, Occupation, AnnualIncome,
                 StreetAddress, City, State, PinCode,LoanDetails,LoanAmount,CIBILScore,
                 OperatedBusinessBefore, BusinessDetails, PreferredFranchiseLocation,
                 OwnProperty, LeasePlan, PropertySizeSqFt, ApproxBudget,
                 AdditionalComments, Created, CreatedBy)
                VALUES
                (@FullName, @Age, @PhoneNumber, @EmailAddress, @Occupation, @AnnualIncome,
                 @StreetAddress, @City, @State, @PinCode,@Loan,@LoanAmount,@Cibil,
                 @OperatedBusinessBefore, @BusinessDetails, @PreferredFranchiseLocation,
                 @OwnProperty, @LeasePlan, @PropertySizeSqFt, @ApproxBudget,
                 @AdditionalComments, @Created, @CreatedBy)";

                SqlCommand cmd = new SqlCommand(query, cn);

                // Add parameters
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Age", Convert.ToInt32(age));
                cmd.Parameters.AddWithValue("@PhoneNumber", phone);
                cmd.Parameters.AddWithValue("@EmailAddress", email);
                cmd.Parameters.AddWithValue("@Occupation", occupation);
                cmd.Parameters.AddWithValue("@AnnualIncome", anualIncome);
                cmd.Parameters.AddWithValue("@StreetAddress", address);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@State", state);
                cmd.Parameters.AddWithValue("@PinCode", pincode);
                cmd.Parameters.AddWithValue("@Loan", loan);
                cmd.Parameters.AddWithValue("@LoanAmount", loanAmount);
                cmd.Parameters.AddWithValue("@Cibil", cibil);
                cmd.Parameters.AddWithValue("@OperatedBusinessBefore", businessExp);
                cmd.Parameters.AddWithValue("@BusinessDetails", businessType);
                cmd.Parameters.AddWithValue("@PreferredFranchiseLocation", location);
                cmd.Parameters.AddWithValue("@OwnProperty", ownProperty);
                cmd.Parameters.AddWithValue("@LeasePlan", leasePlan);
                cmd.Parameters.AddWithValue("@PropertySizeSqFt", string.IsNullOrEmpty(propertySize) ? (object)DBNull.Value : Convert.ToInt32(propertySize));
                cmd.Parameters.AddWithValue("@ApproxBudget", budget);
                cmd.Parameters.AddWithValue("@AdditionalComments", comments);

                // System fields
                cmd.Parameters.AddWithValue("@Created", DateTime.Now.ToString("dd-MM-yyyy"));
                cmd.Parameters.AddWithValue("@CreatedBy", "ADMIN");

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }

        }


        string captchaResponse = Request["g-recaptcha-response"];
        if (!VerifyReCaptcha(captchaResponse))
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "reCAPTCHA validation failed. Please try again.";
            return;
        }

        // Construct email body
        string body = "<h3>Paris Bakery - Franchise Inquiry</h3>";
        body += "<strong>Full Name:</strong> " + fullName + "<br/>";
        body += "<strong>Age:</strong> " + age + "<br/>";
        body += "<strong>Phone:</strong> " + phone + "<br/>";
        body += "<strong>Email:</strong> " + email + "<br/>";
        body += "<strong>Occupation:</strong> " + occupation + "<br/>";
        body += "<strong>Annual Income:</strong> " + anualIncome + "<br/>";
        body += "<strong>Address:</strong> " + address + ", " + city + ", " + state + " - " + pincode + "<br/><br/>";
        body += "<strong>Loan:</strong> " + loan + "<br/>";
        body += "<strong>Loan Amount:</strong> " + loanAmount + "<br/>";
        body += "<strong>Cibil Score:</strong> " + cibil + "<br/>";
        body += "<strong>Business Experience:</strong> " + businessExp + "<br/>";
        body += "<strong>Business Type:</strong> " + businessType + "<br/>";
        body += "<strong>Preferred Location:</strong> " + location + "<br/>";
        body += "<strong>Own Property:</strong> " + ownProperty + "<br/>";
        body += "<strong>Plan to Lease:</strong> " + leasePlan + "<br/>";
        body += "<strong>Property Size (sq. ft.):</strong> " + propertySize + "<br/><br/>";
        body += "<strong>Investment Budget:</strong> " + budget + "<br/>";
        body += "<strong>Additional Comments:</strong> " + comments + "<br/>";

        try
        {
            // ========== 1. Email to Official ========== 
            MailMessage officialMail = new MailMessage();
            officialMail.From = new MailAddress("info@parisbakery.in", "Paris Bakery");
            officialMail.To.Add(adminEmail);
            officialMail.Subject = "New Franchise Inquiry - " + fullName;
            officialMail.IsBodyHtml = true;
            officialMail.Body = body; // detailed info

            // ========== 2. Email to Customer ==========
            MailMessage customerMail = new MailMessage();
            customerMail.From = new MailAddress("info@parisbakery.in", "Paris Bakery");
            customerMail.To.Add(email);
            customerMail.Subject = "Thank You for Your Franchise Inquiry - Paris Bakery";
            customerMail.IsBodyHtml = true;

            string customerBody = "<h3>Dear " + fullName + ",</h3>";
            customerBody += "<p>Thanks for reaching out about joining our franchise family!, <strong>Paris Bakery</strong>.</p>";
            customerBody += "<p>We truly appreciate your interest and will get in touch with you shortly.</p>";
            customerBody += "<br/><p>Warm Regards,<br/>Paris Bakery Team</p>";

            customerMail.Body = customerBody;

            // ========== SMTP Setup ==========
            SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
            smtp.Credentials = new NetworkCredential("info@parisbakery.in", "Yuw22334");
            smtp.EnableSsl = true;

            // ========== Send Both Emails ==========
            smtp.Send(officialMail);
            smtp.Send(customerMail);

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Thank you! Your inquiry has been submitted successfully.";
        }

        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Error sending email. Please try again later.";
        }
        // Clear form fields
        txtFullName.Text = "";
        txtAge.Text = "";
        txtPhone.Text = "";
        txtEmail.Text = "";
        txtAddress.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtPincode.Text = "";
        txtLoan.Text = "";
        txtLoanAmount.Text = "";
        txtCible.Text = "";
        ddlBusinessExp.SelectedIndex = 0;
        txtBusinessType.Text = "";
        txtLocation.Text = "";
        ddlOwnProperty.SelectedIndex = 0;
        ddlLeasePlan.SelectedIndex = 0;
        txtPropertySize.Text = "";
        ddlBudget.SelectedIndex = 0;
        txtComments.Text = "";
        txtAnualIncome.SelectedIndex = 0;
        txtOccupation.Text = "";
    }

}