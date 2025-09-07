using MVC_3_DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVC_3.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			//Mail Server : gmail.com
			//smtp

			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;

			client.Credentials = new NetworkCredential("rewanrabie828@gmail.com", "irkvwjrzlizarvsb");

			client.Send("rewanrabie828@gmail.com",email.To,email.Subject,email.Body);
			

		}
	}
}
