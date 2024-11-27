using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetTrainingBatch5.PointOfSale.DataBase.Models;

public partial class TblProduct
{
   
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int InstockQuantity { get; set; }

    public string? ProductCategoryCode { get; set; }

    public bool DeleteFlag { get; set; }

 
    public virtual TblCategory? ProductCategoryCodeNavigation { get; set; }

    public virtual ICollection<TblSaleDetail> TblSaleDetails { get; set; } = new List<TblSaleDetail>();

    [Timestamp]
    public byte[]? RowVersion { get; set; }
}
