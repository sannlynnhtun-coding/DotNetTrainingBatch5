using DotNetTrainingBatch5.PointOfSale.Api.CombinedReqModel;
using DotNetTrainingBatch5.PointOfSale.Domain;
using DotNetTrainingBatch5.PointOfSale.Domain.Features.SaleDetail;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Product;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Sale;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.SaleDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTrainingBatch5.PointOfSale.Api.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailController : ControllerBase
    {
        private readonly SaleDetailServices _DetailServices = new SaleDetailServices();
        //public SaleDetailController() { 
        
        //_DetailServices = new SaleDetailServices();
        //}
        [HttpPost("Detail")]
        public async Task<Result<ResultSaleDetailResModel>> CreateSaleDetail(CreateDetailReqModel DetailReqItem){

            var result =await _DetailServices.CreateSaleDetailAsync(DetailReqItem.sale.SaleCode,DetailReqItem.product.ProductCode,DetailReqItem.product.SaleQuantity,DetailReqItem.product.Price);

            return result;

        }
        }

}
