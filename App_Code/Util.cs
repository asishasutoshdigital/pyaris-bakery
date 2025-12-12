using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

/// <summary>
///     Summary description for Notifications
/// </summary>
public class Util
{
    public static string Getname(string user)
    {
        string name = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("select [name] from [XUser Details] where [mobile no]='" + user + "'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    name = dr[0].ToString();
                }
            }
            cn.Close();
        }
        catch (Exception)
        {
            name = "";
        }
        return name;
    }

    public static string GetCustomeruser(string OrderNumber)
    {
        string CustomerUser = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("select [User] FROM [XSales Master] WHERE [Order No]= '" + OrderNumber + "'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    CustomerUser = dr[0].ToString();
                }
            }
            cn.Close();
        }
        catch (Exception)
        {
            CustomerUser = "";
        }
        return CustomerUser;
    }
    public static PaymentDetails GetPaymentDetails(string orderNumber)
    {
        var cn = new SqlConnection(Program.Connection);
        PaymentDetails payDetails = new PaymentDetails();

        try
        {
            cn.Open();
            var cmd1x = new SqlCommand(
                     "select [Paymode],sm.[Transaction], pg.[PG Request ID], pg.[Payment Amount] from [xSales MAster] sm inner join [PG Provider Instamojo] PG on sm.[Transaction] = PG.[Transaction]  where pg.[Payment Status] ='SUCCESS' and sm.[Order No]='" + orderNumber + "'", cn);
            var sqlpaymentDetails = cmd1x.ExecuteReader();
            if (sqlpaymentDetails.HasRows)
            {
                if (sqlpaymentDetails.Read())
                {
                    payDetails.Paymentmode = sqlpaymentDetails[0].ToString();
                    payDetails.PyarisTransactionId = sqlpaymentDetails[1].ToString();
                    payDetails.PhonepeTransactionNumber = sqlpaymentDetails[2].ToString();
                    payDetails.Amount = sqlpaymentDetails[3].ToString() + "00";

                }
            }
            cn.Close();
        }
        catch (Exception) { }

        return payDetails;
    }

    public static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}