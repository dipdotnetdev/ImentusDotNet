namespace CoreWebAPIPract.DI
{
    public class OrderService
    {
        private readonly INotificationService _service;

        public OrderService(INotificationService service)
        {
            _service = service;
        }

        public void PlaceOrder()
        {
            _service.Send();
        }
    }
}