using Notebook.Data.Interface;
using Notebook.Data.Model;
using System;
using System.Net;
using System.Net.Mail;

namespace Notebook.Data.Service
{
    public class EmailService : IEmail
    {
        // Config email
        private readonly string _hostName = "smtp.example.com";
        private readonly string _adminMail = "admin@example.com";
        private readonly string _adminMailPass = "password";
        private readonly int _port = 25;
        private readonly bool _enableSsl = true;

        public void SendEmail(EmailLetter letter)
        {
            if (letter == null || String.IsNullOrEmpty(letter.Receiver))
                throw new ArgumentException("Wrong letter information.");

            using (var message = new MailMessage(_adminMail, letter.Receiver))
            {
                message.Subject = letter.Subject;
                message.Body = letter.Message;

                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = _enableSsl,
                    Host = _hostName,
                    Port = _port,
                    Credentials = new NetworkCredential(_adminMail, _adminMailPass)
                })
                {
#if (DEBUG)
                    Console.WriteLine("\n" + letter.Subject + "\n" + letter.Message + "\n");
#else
                    client.Send(message);
#endif
                }
            }
        }
    }
}
