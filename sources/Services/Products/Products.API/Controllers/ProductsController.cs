using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Products.API.Domain.Dtos;
using Products.API.Domain.Service.Interfaces;
using Products.API.Infra.Filters;
using Products.API.Infra.Queries.Interfaces;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromServices] IGetAllProductsQuery getAllProducts, [FromQuery] ProductFilter productFilter) =>
            Ok(getAllProducts.Get(productFilter));

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromServices] ISaveProductService saveProductService, [FromBody] ProductToSaveDto dto)
        {
            var savedProductDto = await saveProductService.Save(dto);

            if (savedProductDto == null)
                return BadRequest();

            return Ok(savedProductDto);
        }
    }
}