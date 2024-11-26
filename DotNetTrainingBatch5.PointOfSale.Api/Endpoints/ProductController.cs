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

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                var result = await _service.GetProductsAsync();
               
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var result = await _service.GetProductAsync(id);
                if (result is null) 
                { 
                    return NotFound("No data found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
       [HttpPost ("create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductReqModel reqModel)
        {
            try
            {
                var result = await _service.CreateProductAsync( reqModel.ProductCode, reqModel.ProductName, reqModel.Price,reqModel.InstockQuantity );
                return Ok(result);
            }
            catch (Exception ex)
            {
             
                return StatusCode(500, new {error = ex.Message});
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
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _service.DeleteProductAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
