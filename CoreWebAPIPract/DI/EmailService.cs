namespace CoreWebAPIPract.DI
{
    public class EmailService : INotificationService
    {
        public void Send()
        {
            Console.WriteLine("Email sent");
        }
    }
}
