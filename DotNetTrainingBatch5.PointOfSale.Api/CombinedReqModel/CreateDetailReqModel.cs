using DotNetTrainingBatch5.PointOfSale.Domain.Models.Product;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Sale;

namespace DotNetTrainingBatch5.PointOfSale.Api.CombinedReqModel;

public class CreateDetailReqModel
{
    public SaleProductReqModel product { get; set; }
    public SaleReqModel sale {  get; set; }
}
