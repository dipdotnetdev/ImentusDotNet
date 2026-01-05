namespace CoreWebAPIPract.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Product { get; set; }
    }
}
