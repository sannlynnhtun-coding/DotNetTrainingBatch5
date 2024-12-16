namespace DotNetTrainingBatch5.PointOfSale.Api.Endpoints;

[Route("api/[controller]")]
[ApiController]
public class SaleDetailController : ControllerBase
{
   /* private readonly SaleDetailServices _DetailServices = new SaleDetailServices();
    //public SaleDetailController() { 
    
    //_DetailServices = new SaleDetailServices();
    //}
    [HttpPost("Detail")]
    public async Task<Result<ResultSaleDetailResModel>> CreateSaleDetail(CreateDetailReqModel DetailReqItem){

        var result =await _DetailServices.CreateSaleDetailAsync(DetailReqItem.sale.SaleCode,DetailReqItem.product.ProductCode,DetailReqItem.product.SaleQuantity,DetailReqItem.product.Price);

        return result;

    }*/



}
