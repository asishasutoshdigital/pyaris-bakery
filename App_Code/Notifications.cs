using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///     Summary description for Notifications
/// </summary>
public class Notifications
{
    public static void SaveNotification(string message)
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd =
                new SqlCommand(
                    "insert into [Notification] values ('" + DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy") +
                    "','" + message.ToUpper() + "')", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch (Exception ex)
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }
    }
}