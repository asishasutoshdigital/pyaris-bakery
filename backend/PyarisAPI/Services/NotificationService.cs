using System.Data;
using System.Data.SqlClient;

namespace PyarisAPI.Services
{
    public class NotificationService
    {
        private readonly string _connectionString;

        public NotificationService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public void SaveNotification(string message)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                try
                {
                    cn.Open();
                    var cmd = new SqlCommand(
                        $"insert into [Notification] values ('{DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy")}','{message.ToUpper().Replace("'", "''")}')", 
                        cn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogService.Error("Error saving notification", ex);
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
                    var cmd = new SqlCommand($"SELECT TOP {count} [Message] FROM [Notification] ORDER BY [Date] DESC", cn);
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        notifications.Add(dr[0].ToString() ?? "");
                    }
                }
                catch (Exception ex)
                {
                    LogService.Error("Error fetching notifications", ex);
                }
            }
            return notifications;
        }
    }
}
