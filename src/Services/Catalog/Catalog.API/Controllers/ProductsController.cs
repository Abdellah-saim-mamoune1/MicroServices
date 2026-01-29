using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _userService;

        public ProductsController(ProductService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<Product>> Get() =>
            await _userService.GetAsync();

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto product)
        {
            await _userService.CreateAsync(product);
            return Ok();
        }

    }
}
