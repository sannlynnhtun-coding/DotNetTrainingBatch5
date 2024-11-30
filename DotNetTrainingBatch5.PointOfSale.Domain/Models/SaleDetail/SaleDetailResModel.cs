using DotNetTrainingBatch5.PointOfSale.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Models.SaleDetail
{
    public class SaleDetailResModel
    {

        public List<TblSaleDetail> SaleDeails { get; set; }
    }
    public class ResultSaleDetailResModel
    {

        public List<TblSaleDetail> SaleDeails { get; set; }
    }
}
