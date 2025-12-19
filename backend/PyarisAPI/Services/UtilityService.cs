using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using PyarisAPI.Models;

namespace PyarisAPI.Services
{
    public class UtilityService
    {
        private readonly string _connectionString;
        private readonly LogService _logService;

        public UtilityService(IConfiguration configuration, LogService logService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logService = logService;
        }

        public string GetName(string user)
        {
            string name = "";

            using (var cn = new SqlConnection(_connectionString))
            {
                try
                {
                    cn.Open();
                    var cmd = new SqlCommand(
                        $"select [name] from [XUser Details] where [mobile no]='{user.Replace("'", "''")}'",
                        cn);

                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        name = dr[0]?.ToString() ?? "";
                    }
                }
                catch (Exception ex)
                {
                    _logService.Error("Error fetching user name", ex);
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
                    var cmd = new SqlCommand(
                        $"select [User] FROM [XSales Master] WHERE [Order No]= '{orderNumber.Replace("'", "''")}'",
                        cn);

                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        customerUser = dr[0]?.ToString() ?? "";
                    }
                }
                catch (Exception ex)
                {
                    _logService.Error("Error fetching customer user", ex);
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
                    var cmd = new SqlCommand(
                        $"select [Paymode], sm.[Transaction], pg.[PG Request ID], pg.[Payment Amount] " +
                        $"from [xSales MAster] sm " +
                        $"inner join [PG Provider Instamojo] PG on sm.[Transaction] = PG.[Transaction] " +
                        $"where pg.[Payment Status] ='SUCCESS' and sm.[Order No]='{orderNumber.Replace("'", "''")}'",
                        cn);

                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        payDetails.TransactionId = dr[1]?.ToString() ?? "";
                        payDetails.OrderId = orderNumber;
                        payDetails.Amount = decimal.Parse(dr[3]?.ToString() ?? "0");
                        payDetails.Status = "SUCCESS";
                        payDetails.PaymentMethod = dr[0]?.ToString() ?? "";
                    }
                }
                catch (Exception ex)
                {
                    _logService.Error("Error fetching payment details", ex);
                }
            }

            return payDetails;
        }

        // Static utility method is perfectly fine
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
