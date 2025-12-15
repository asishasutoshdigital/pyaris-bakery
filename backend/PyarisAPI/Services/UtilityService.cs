using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using PyarisAPI.Models;

namespace PyarisAPI.Services
{
    public class UtilityService
    {
        private readonly string _connectionString;

        public UtilityService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public string GetName(string user)
        {
            string name = "";
            using (var cn = new SqlConnection(_connectionString))
            {
                try
                {
                    cn.Open();
                    var cmd = new SqlCommand($"select [name] from [XUser Details] where [mobile no]='{user.Replace("'", "''")}'", cn);
                    var dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            name = dr[0].ToString() ?? "";
                        }
                    }
                }
                catch (Exception)
                {
                    name = "";
                }
            }
            return name;
        }

        public string GetCustomerUser(string orderNumber)
        {
            string customerUser = "";
            using (var cn = new SqlConnection(_connectionString))
            {
                try
                {
                    cn.Open();
                    var cmd = new SqlCommand($"select [User] FROM [XSales Master] WHERE [Order No]= '{orderNumber.Replace("'", "''")}'", cn);
                    var dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            customerUser = dr[0].ToString() ?? "";
                        }
                    }
                }
                catch (Exception)
                {
                    customerUser = "";
                }
            }
            return customerUser;
        }

        public PaymentDetails GetPaymentDetails(string orderNumber)
        {
            var payDetails = new PaymentDetails();
            using (var cn = new SqlConnection(_connectionString))
            {
                try
                {
                    cn.Open();
                    var cmd1x = new SqlCommand(
                        $"select [Paymode],sm.[Transaction], pg.[PG Request ID], pg.[Payment Amount] from [xSales MAster] sm inner join [PG Provider Instamojo] PG on sm.[Transaction] = PG.[Transaction]  where pg.[Payment Status] ='SUCCESS' and sm.[Order No]='{orderNumber.Replace("'", "''")}'", cn);
                    var sqlpaymentDetails = cmd1x.ExecuteReader();
                    if (sqlpaymentDetails.HasRows)
                    {
                        if (sqlpaymentDetails.Read())
                        {
                            // Map to PaymentDetails properties as needed
                            payDetails.TransactionId = sqlpaymentDetails[1].ToString() ?? "";
                            payDetails.OrderId = orderNumber;
                            payDetails.Amount = decimal.Parse(sqlpaymentDetails[3].ToString() ?? "0");
                            payDetails.Status = "SUCCESS";
                            payDetails.PaymentMethod = sqlpaymentDetails[0].ToString() ?? "";
                        }
                    }
                }
                catch (Exception) { }
            }
            return payDetails;
        }

        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
