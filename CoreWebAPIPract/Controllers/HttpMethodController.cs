using CoreWebAPIPract.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPIPract.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HttpMethodController : ControllerBase
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Price = 75000 },
            new Product { Id = 2, Name = "Phone", Price = 35000 }
        };

        [HttpGet]
        public IActionResult GetAll()
        {   
            return Ok(_products);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)    
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Product product)
        {
            if(id != product.Id)
                return BadRequest("Id not found");

            var existingProduct = _products.FirstOrDefault(p =>  p.Id == id);
            if(existingProduct == null)
                return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(_ => _.Id == id);
            if(product == null)
                return NotFound();

            _products.Remove(product);
            return NoContent();
        }

        //[HttpPatch("{id:int}")]
        //public IActionResult Patch(
        //int id,
        //JsonPatchDocument<Product> patchDocument)
        //{
        //    if (patchDocument == null)
        //        return BadRequest();

        //    var product = _products.FirstOrDefault(p => p.Id == id);
        //    if (product == null)
        //        return NotFound();

        //    patchDocument.ApplyTo(product, ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return NoContent();
        //}

        [HttpHead("{id:int}")]
        public IActionResult Head(int id)
        {
            var exists = _products.Any(p => p.Id == id);
            return exists ? Ok() : NotFound();
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
