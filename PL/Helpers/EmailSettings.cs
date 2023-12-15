using System.Net;
using System.Net.Http;
using System.Net.Mail;

namespace PL.Helpers
{
    public static class EmailSettings
    {
       public static void Send(Email email)
       {
            var sender = "mostafashalaanovic@gmail.com";
            var host = "smtp.gmail.com";
            var senderPassword = "ktco oqge pwcb kwbs";
            var client = new SmtpClient(host)
            {
                Credentials=new NetworkCredential(sender, senderPassword),
                Port= 587,
                EnableSsl = true
            };

            client.Send(sender,email.To,email.Title,email.Body);
       }
    }
}
