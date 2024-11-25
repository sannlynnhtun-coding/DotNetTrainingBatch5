using DotNetTrainingBatch5.PointOfSale.DataBase.Models;
using DotNetTrainingBatch5.PointOfSale.Domain.Features.Products;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.Api.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet ]

        [HttpPost ("create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductReqModel reqModel)
        {
            try
            {
                var result = await _service.CreateProductAsync( reqModel.ProductCode, reqModel.ProductName, reqModel.Price);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }


        [HttpPatch]
        public async Task<IActionResult> EditProduct([FromBody] ProductReqModel reqModel)
        {
            try
            {
                var result = await _service.UpdateProductAsync(reqModel.ProductId,reqModel.ProductCode, reqModel.ProductName, reqModel.Price);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
