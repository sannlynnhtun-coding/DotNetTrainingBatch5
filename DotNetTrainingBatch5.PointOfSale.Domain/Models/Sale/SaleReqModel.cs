using DotNetTrainingBatch5.PointOfSale.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Models.Sale
{
    public class SaleReqModel
    {
        public string? SaleCode { get; set; } = null;

        public DateTime? SaleDate { get; set; }

        public decimal? TotalSale { get; set; }

        public decimal? PayAmount { get; set; }

        public decimal? ChangeAmount { get { return TotalSale - PayAmount; } }

        public static decimal calculateTotalSale(List<TblSaleDetail> saleDetails)
        {
            decimal totalSale = 0;

            foreach (var i in saleDetails)
            {
                totalSale += i.Total;
            }

            return totalSale;
        }
    }
}
