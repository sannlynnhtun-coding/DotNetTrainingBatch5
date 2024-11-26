using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Models.Sale
{
    public class SaleReqModel
    {

        public decimal PayAmount { get; set; }
        public string? SaleCode { get; set; } = null;

    }
}
