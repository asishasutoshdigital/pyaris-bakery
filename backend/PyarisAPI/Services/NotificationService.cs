using System.Data;
using System.Data.SqlClient;

namespace PyarisAPI.Services
{
    public class NotificationService
    {
        private readonly string _connectionString;
        private readonly LogService _logService;

        public NotificationService(IConfiguration configuration, LogService logService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logService = logService;
        }

        public void SaveNotification(string message)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                try
                {
                    cn.Open();
                    var cmd = new SqlCommand(
                        $"insert into [Notification] values ('{DateTime.UtcNow.AddHours(5.5):dd-MM-yyyy}','{message.ToUpper().Replace("'", "''")}')",
                        cn);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _logService.Error("Error saving notification", ex);
                }
            }
        }

        public List<string> GetRecentNotifications(int count = 10)
        {
            var notifications = new List<string>();

            using (var cn = new SqlConnection(_connectionString))
            {
                try
                {
                    cn.Open();
                    var cmd = new SqlCommand(
                        $"SELECT TOP {count} [Message] FROM [Notification] ORDER BY [Date] DESC",
                        cn);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            notifications.Add(dr[0]?.ToString() ?? "");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logService.Error("Error fetching notifications", ex);
                }
            }

            return notifications;
        }
    }
}
