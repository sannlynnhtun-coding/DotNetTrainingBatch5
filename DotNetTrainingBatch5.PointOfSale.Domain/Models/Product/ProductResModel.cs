using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetTrainingBatch5.PointOfSale.DataBase.Models;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Models.Product
{
 
        public class ResultProductResponseModel
        {

            public List<TblProduct?> Product { get; set; }

        }
 
}
