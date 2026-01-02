using Microsoft.Extensions.Options;

namespace CoreWebAPIPract.Option_Pattern
{
    public class EmailServices
    {
        private readonly EmailSettings emailSettings; 

        public EmailServices(IOptions<EmailSettings> options)
        {
            emailSettings = options.Value;
        }

        public void Send()
        {
            var smtpServer = emailSettings.SMTPServer;
            Console.WriteLine($"{smtpServer}");
        }

    }
}
