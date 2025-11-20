using System.Net.Mail;

namespace MonitoramentoEscolarAPI.Services
{
    public class EnviarEmailService
    {
        private readonly IConfiguration _configuration;

        public EnviarEmailService(IConfiguration configuration)
        {
           _configuration = configuration;

        }

        public async Task EnviarEmailAsync(string Para, string Assunto, string HTML)
        {
            var email = _configuration["Gmail:Email"];
            var senha = _configuration["Gmail:SenhaApp"];

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(email, senha),
                EnableSsl = true,
            };

            var messagem = new MailMessage
            {
                From = new MailAddress(email, "Monitoramento escolar"),
                Subject = Assunto,
                Body = HTML,
                IsBodyHtml = true

            };

            messagem.To.Add(Para);


            await smtp.SendMailAsync(messagem);
        }

    }
}
