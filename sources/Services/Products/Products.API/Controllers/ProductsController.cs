using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Products.API.Infra.Filters;
using Products.Infra.Queries.Interfaces;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;        

        public ProductsController(
            ILogger<ProductsController> logger
        )
        {
            _logger = logger;
        }

        [HttpGet]
        //[AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(
            [FromServices] IGetAllProductsQuery getAllProducts,
            [FromQuery] ProductFilter productFilter
        ) => Ok(getAllProducts.Get(productFilter));
    }
}