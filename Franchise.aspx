<%@ Page Language="C#" MasterPageFile="~/Pyaris.master" AutoEventWireup="true" CodeFile="Franchise.aspx.cs" Inherits="Franchise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
    window.onload = function () {
        var msg = document.getElementById('<%= lblMessage.ClientID %>');
        if (msg && msg.innerText.trim() !== "") {
            setTimeout(function () {
                msg.style.display = 'none';
            }, 10000); // hides after 10 seconds
        }
        };

        function allowOnlyNumbers(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;

            // Block space (charCode 32) and non-digit keys
            if (charCode < 48 || charCode > 57) {
                evt.preventDefault();
            }
        }

    </script>
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>


    <style>
.custom-form h2 {
    color: #d63384;
    text-align: center;
    font-size: 32px;
    margin-bottom: 10px;
}

.custom-form .intro {
    text-align: center;
    margin-bottom: 40px;
    color: #555;
    font-size: 16px;
}

.custom-form .form-section {
    background-color: #ffffff;
    padding: 35px;
    border-radius: 18px;
    max-width: 900px;
    margin: auto;
    box-shadow: 0 8px 24px rgba(0, 0, 0, 0.08);
    transition: all 0.3s ease;
}

.custom-form .section-title {
    color: #d63384;
    font-size: 17px;
    border-left: 5px solid #d63384;
    padding-left: 10px;
    margin-top: 25px;
    margin-bottom: 20px;
}

.custom-form .form-row {
    display: flex;
    gap: 20px;
    flex-wrap: wrap;
}

.custom-form .form-col {
    flex: 1;
    min-width: 280px;
}

.custom-form label {
    display: block;
    margin-bottom: 6px;
    font-weight: 600;
    color: #444;
}

.custom-form input[type="text"],
.custom-form input[type="number"],
.custom-form input[type="email"],
.custom-form input[type="tel"],
.custom-form select,
.custom-form textarea,
.custom-form .aspnet-control {
    width: 100%;
    padding: 12px 16px;
    font-size: 15px;
    border: 1px solid #ccc;
    border-radius: 10px;
    margin-bottom: 2px;
    transition: border-color 0.2s ease;
}

.custom-form input:focus,
.custom-form select:focus,
.custom-form textarea:focus {
    border-color: #d63384;
    outline: none;
    box-shadow: 0 0 0 2px rgba(214, 51, 132, 0.15);
}

.custom-form textarea {
    resize: vertical;
    min-height: 80px;
}

.custom-form .btn-submit {
    background-color: #d63384;
    margin-top:10px;
    color: white;
    padding: 14px 28px;
    font-size: 16px;
    border: none;
    border-radius: 10px;
    cursor: pointer;
    width: 100%;
    transition: background-color 0.3s ease, transform 0.2s;
}

.custom-form .btn-submit:hover {
    background-color: #bd246e;
    transform: translateY(-2px);
}

@media (max-width: 600px) {
    .custom-form .form-row {
        flex-direction: column;
    }
}

.custom-form .message-label {
    margin-top: 15px;
    display: block;
    font-weight: bold;
    color: green;
}


    </style>
    <div class="custom-form">
    <h2>Paris Bakery - Franchise Inquiry Form</h2>
    <div class="intro">
        Paris Bakery is one of India's fastest-growing premium bakery chains, delivering joy through handcrafted cakes, pastries, and snacks.
    </div>


    <div class="form-section">
        <asp:Label ID="lblMessage" runat="server" CssClass="message-label" EnableViewState="false"></asp:Label>

        <%-- Personal Info --%>
        <h3 class="section-title">1. Personal Info</h3>
        <div class="form-row">
            <div class="form-col">
                <label>Full Name <span style="color:red">*</span></label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="aspnet-control" required="true"></asp:TextBox>
            </div>
            <div class="form-col">
                <label>Age <span style="color:red">*</span></label>
                <asp:TextBox ID="txtAge" runat="server" CssClass="aspnet-control" min="0" TextMode="Number" onkeypress="allowOnlyNumbers(event)" required="true"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-col">
                <label>Phone Number <span style="color:red">*</span></label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="aspnet-control" MaxLength="10" onkeypress="allowOnlyNumbers(event)" required="true"></asp:TextBox>
            </div>
            <div class="form-col">
                <label>Email Address <span style="color:red">*</span></label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="aspnet-control" TextMode="Email" required="true"></asp:TextBox>
            </div>
        </div>

        <div class="form-row">
            <div class="form-col">
                <label>Occupation <span style="color:red">*</span></label>
                <asp:TextBox ID="txtOccupation" runat="server" CssClass="aspnet-control" required="true"></asp:TextBox>
            </div>
          <div class="form-col">
                <label>Annual Income <span style="color:red">*</span></label>
                <asp:DropDownList ID="txtAnualIncome" runat="server" CssClass="aspnet-control" required="true">
                    <asp:ListItem Text="--Select--" Value="" />
                    <asp:ListItem Text="Below 10 Lakhs" Value="Below 10 Lakhs" />
                    <asp:ListItem Text="10 to 20 Lakhs" Value="10 to 20 Lakhs" />                    
                    <asp:ListItem Text="Above 10 Lakhs" Value="Above 10 Lakhs" />
                </asp:DropDownList>
              <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>

        <%--Location  --%>
        <h3 class="section-title">2. Location</h3>
        
        <label>Street Address <span style="color:red">*</span></label>
        <asp:TextBox ID="txtAddress" runat="server" CssClass="aspnet-control" required="true"></asp:TextBox>
        
        <div class="form-row">
            <div class="form-col">
                <label>City <span style="color:red">*</span></label>
                <asp:TextBox ID="txtCity" runat="server" CssClass="aspnet-control" required="true"></asp:TextBox>
            </div>
            <div class="form-col">
                <label>State <span style="color:red">*</span></label>
                <asp:TextBox ID="txtState" runat="server" CssClass="aspnet-control" required="true"></asp:TextBox>
            </div>
        </div>
        
        <label>Pin Code <span style="color:red">*</span></label>
        <asp:TextBox ID="txtPincode" runat="server" CssClass="aspnet-control" onkeypress="allowOnlyNumbers(event)" MaxLength="6" required="true"></asp:TextBox>


        
        <%--Loan Deatils--%>
        <h3 class="section-title">3. Loan</h3>
         
        <label>Loan(if any)</label>
        <asp:TextBox ID="txtLoan" runat="server" CssClass="aspnet-control"></asp:TextBox>        

        <label>Loan Amount (INR)</label>
        <asp:TextBox ID="txtLoanAmount" runat="server" onkeypress="allowOnlyNumbers(event)" CssClass="aspnet-control"></asp:TextBox>       
        
        <label>CIBIL Score</label>
        <asp:TextBox ID="txtCible" runat="server" onkeypress="allowOnlyNumbers(event)"  MaxLength="3" CssClass="aspnet-control"></asp:TextBox>

        <%-- Investment --%>

        <h3 class="section-title">4. Investment</h3>

        <label>Have you operated any business before? <span style="color:red">*</span></label>
        <asp:DropDownList ID="ddlBusinessExp" runat="server" CssClass="aspnet-control" required="true">
            <asp:ListItem Text="--Select--" Value="" />
            <asp:ListItem Text="Yes" Value="Yes" />
            <asp:ListItem Text="No" Value="No" />
        </asp:DropDownList>

        <label>If Yes, please specify</label>
        <asp:TextBox ID="txtBusinessType" runat="server" CssClass="aspnet-control"></asp:TextBox>

        <label>Preferred Franchise Location <span style="color:red">*</span></label>
        <asp:TextBox ID="txtLocation" runat="server" CssClass="aspnet-control" required="true"></asp:TextBox>

        <div class="form-row">
            <div class="form-col">
                <label>Do you own the property? <span style="color:red">*</span></label>
                <asp:DropDownList ID="ddlOwnProperty" runat="server" CssClass="aspnet-control">
                    <asp:ListItem Text="--Select--" Value="" />
                    <asp:ListItem Text="Yes" Value="Yes" />
                    <asp:ListItem Text="No" Value="No" />
                </asp:DropDownList>
            </div>
            <div class="form-col">
                <label>If not, are you planning to lease?</label>
                <asp:DropDownList ID="ddlLeasePlan" runat="server" CssClass="aspnet-control">
                    <asp:ListItem Text="--Select--" Value="" />
                    <asp:ListItem Text="Yes" Value="Yes" />
                    <asp:ListItem Text="No" Value="No" />
                </asp:DropDownList>
            </div>
        </div>

        <label>Property Size (sq. ft.)</label>
        <asp:TextBox ID="txtPropertySize" runat="server" min="0" CssClass="aspnet-control" onkeypress="allowOnlyNumbers(event)" TextMode="Number"></asp:TextBox>

        <label>Approximate Budget for Investment (INR) <span style="color:red">*</span></label>
        <asp:DropDownList ID="ddlBudget" runat="server" CssClass="aspnet-control" required="true">
            <asp:ListItem Text="--Select--" Value="" />
            <asp:ListItem Text="₹10 – ₹15 Lakhs" Value="10 to 15 Lakhs" />
            <asp:ListItem Text="₹15 – ₹20 Lakhs" Value="15 to 20 Lakhs" />
            <asp:ListItem Text="₹20 Lakhs and above" Value="Above 20 Lakhs" />
        </asp:DropDownList>

        <label>Additional Comments or Questions</label>
        <asp:TextBox ID="txtComments" runat="server" CssClass="aspnet-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
        <asp:Literal ID="ltrReCaptcha" runat="server"></asp:Literal>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit Inquiry" CssClass="btn-submit" OnClick="btnSubmit_Click" />
    </div>
        </div>
</asp:Content>

