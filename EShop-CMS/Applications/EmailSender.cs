using System.Net;
using System.Net.Mail;

namespace EShop_CMS.Applications
{
    public class EmailSender
    {
        public void SendOTP(string reciever)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("atsr58227@gmail.com");
            mail.To.Add(reciever);
            mail.Subject = "EShop Verfication | Do not reply";
            mail.Body = "Thanks for joining us !" + "Your code is :  "+OTPGenerator();

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(
                "atsr58227@gmail.com",
                "ezfv juen kawb fdkb"
            );

            try
            {
                smtp.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public string OTPGenerator()
        {
            Random rnd = new Random();
            var result= rnd.Next(1234,9876);
            return result.ToString();
        }
    }
}
