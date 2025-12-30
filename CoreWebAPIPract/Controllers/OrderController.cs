using CoreWebAPIPract.DI;
using CoreWebAPIPract.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPIPract.Controllers
{
    [ApiController]
    [Route("api/orders")]
    //[LogAction]
    public class OrderController : ControllerBase
    {
        public readonly OrderService _order;

        public OrderController(OrderService order)
        {
            _order = order;
        }

        public IActionResult PlaceOrder()
        {
            _order.PlaceOrder();
            return Ok();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOrder(int id) => Ok();

        [HttpPost("/createOrder")]
        public IActionResult CreateOrder() => Ok();

        //[HttpGet("{year:int}/{month:int}")]
        //public IActionResult OrderByMonth(int month, int year) => Ok();

        [HttpPost("/create")]
        public IActionResult Create([FromBody] Product product) => Ok();

        //[HttpGet("/id")]
        //public IActionResult Get([FromRoute] int id) => Ok();

        //[HttpPost("/upload")]
        //public IActionResult Upload([FromForm] IFormFile file) => Ok();
    }
}
