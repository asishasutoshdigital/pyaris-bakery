using System.Data.SqlClient;

namespace PyarisAPI.Services
{
    public class PromoService
    {
        private readonly string _connectionString;
        private readonly LogService _logService;

        public PromoService(IConfiguration configuration, LogService logService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logService = logService;
        }

        public bool ValidatePromoCode(string promoCode, string userId)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                try
                {
                    cn.Open();
                    var cmd = new SqlCommand(
                        $"SELECT COUNT(*) FROM [PromoCodes] WHERE [Code]='{promoCode.Replace("'", "''")}' AND [Active]=1 AND [ExpiryDate] >= GETDATE()",
                        cn);

                    var count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    _logService.Error("Error validating promo code", ex);
                    return false;
                }
            }
        }

        public decimal GetPromoDiscount(string promoCode)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                try
                {
                    cn.Open();
                    var cmd = new SqlCommand(
                        $"SELECT [DiscountAmount] FROM [PromoCodes] WHERE [Code]='{promoCode.Replace("'", "''")}' AND [Active]=1",
                        cn);

                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
                catch (Exception ex)
                {
                    _logService.Error("Error getting promo discount", ex);
                    return 0;
                }
            }
        }
    }
}
