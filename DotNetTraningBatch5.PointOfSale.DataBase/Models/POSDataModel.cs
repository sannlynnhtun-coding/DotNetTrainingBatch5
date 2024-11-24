using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.DataBase.Models
{
    public class POSDataModel
    {

        [Table("Tbl_Product")]
        public class ProductDataModel
        {
            [Key]
            [Column("ProductId")]
            public int ProductId { get; set; }

            [Column("ProductName")]
            public string? ProductName { get; set; }

            [Column("ProductCode")]
            public string? ProductCode { get; set; }

            [Column("Price")]
            public decimal Price { get; set; }

            [Column("DeleteFlag")]
            public bool DeleteFlag { get; set; }
        }
    }
}
