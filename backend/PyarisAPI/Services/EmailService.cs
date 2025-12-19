using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Data.SqlClient;
using System.Security.Authentication;

namespace PyarisAPI.Services
{
    public class EmailService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly LogService _logService;

        private readonly string _emailUserName;
        private readonly string _emailPassword;
        private readonly string _emailEnvironment;

        public EmailService(IConfiguration configuration, LogService logService)
        {
            _configuration = configuration;
            _logService = logService;

            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _emailUserName = configuration["Email:UserName"] ?? "";
            _emailPassword = configuration["Email:Password"] ?? "";
            _emailEnvironment = configuration["Email:Environment"] ?? "";
        }

        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("Paris Bakery", _emailUserName));

                if (!string.IsNullOrEmpty(to))
                {
                    var mailboxAddresses = new List<MailboxAddress>();
                    string[] values = to.Split(';');

                    foreach (var email in values)
                    {
                        var trimmedEmail = email.Trim();
                        if (!string.IsNullOrEmpty(trimmedEmail))
                        {
                            mailboxAddresses.Add(
                                new MailboxAddress(trimmedEmail, trimmedEmail));
                        }
                    }

                    mimeMessage.To.AddRange(mailboxAddresses);
                }

                mimeMessage.Subject = _emailEnvironment + " " + subject;
                var builder = new BodyBuilder { HtmlBody = htmlBody };
                mimeMessage.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.CheckCertificateRevocation = false;
                    client.SslProtocols =
                        SslProtocols.Ssl3 |
                        SslProtocols.Tls |
                        SslProtocols.Tls11 |
                        SslProtocols.Tls12;

                    await client.ConnectAsync(
                        "smtp.office365.com",
                        587,
                        SecureSocketOptions.StartTls);

                    await client.AuthenticateAsync(_emailUserName, _emailPassword);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                _logService.Error("Error sending email", ex);
                throw;
            }
        }

        public string FetchEmail(string username)
        {
            string email = "";

            using (var cn = new SqlConnection(_connectionString))
            {
                try
                {
                    cn.Open();
                    var cmd = new SqlCommand(
                        $"select [Email] from [xUser Details] where [Mobile No]='{username.Replace("'", "''")}'",
                        cn);

                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        email = dr[0]?.ToString() ?? "";
                    }
                }
                catch (Exception ex)
                {
                    _logService.Error("Error fetching email", ex);
                }
            }

            return email;
        }
    }
}
