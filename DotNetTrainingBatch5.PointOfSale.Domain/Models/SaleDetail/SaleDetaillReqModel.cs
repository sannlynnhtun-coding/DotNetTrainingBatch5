using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Models.SaleDetail
{
    public class SaleDetailReqModel
    {

        public string? DetailCode { get; set; }

        public string? ProductCode { get; set; } = "";

        public string? SaleCode { get; set; } = "";

        public int ProductQuantity { get; set; } = 0;

        public decimal Total { get; set; } = 0M;

    }
}
