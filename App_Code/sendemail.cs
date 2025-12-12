using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Authentication;

/// <summary>
/// Summary description for sendemail
/// </summary>
public class sendemail
{
    public sendemail()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static void Gox(string to, string subject, string data)
    {
        try
        {
            string body = data;
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Paris Bakery", Program.EmailServer_UserName));
            if (to != null)
            {
                var mailboxAddress = new List<MailboxAddress>();
                string[] values = to.Split(';');
                for (int i = 0; i < values.Length; i++)
                {
                    mailboxAddress.Add(new MailboxAddress(values[i].Trim(), values[i].Trim()));
                }
                mimeMessage.To.AddRange(mailboxAddress);
            }
            mimeMessage.Subject = Program.Email_Environment + " " + subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = data;
            mimeMessage.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.CheckCertificateRevocation = false;
                client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
                client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                // Note: only needed if the SMTP server requires authentication 
                // Error 5.5.1 Authentication  
                client.Authenticate(Program.EmailServer_UserName, Program.EmailServer_Password);
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
        }
        catch (Exception EX)
        {
            WebMsgBox.Show(EX.Message);
        }
    }


    public static string Fetchemail(string username)
    {
        string data = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("select [Email] from [xUser Details] where [Mobile No]='" + username + "'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    data = dr[0].ToString();
                }
            }
            cn.Close();
        }
        catch (Exception)
        {
            data = "";
        }
        return data;
    }


    public static string SaveFathersday(string name, string mob, string email, string fathername, string message)
    {
        string data = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("insert into [Spl_Father_Day] values ('" + DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy") +
                    "','" + DateTime.UtcNow.AddHours(5.5).ToString("HH:mm") +
                    "','" + name.ToUpper() + "','" + mob + "','" + email.ToUpper() + "','" + fathername.ToUpper() + "','" + message + "')", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch (Exception)
        {
            data = "";
        }
        return data;
    }
}