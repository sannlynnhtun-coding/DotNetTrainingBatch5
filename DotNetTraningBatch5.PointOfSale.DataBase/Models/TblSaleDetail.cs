using System;
using System.Collections.Generic;

namespace DotNetTrainingBatch5.PointOfSale.DataBase.Models;

public partial class TblSaleDetail
{
    public int DetailId { get; set; }

    public string? DetailCode { get; set; }

    public string ProductCode { get; set; } = null!;

    public int ProductQuantity { get; set; }

    public decimal Total { get; set; }

    public string SaleCode { get; set; } = null!;

    public bool DeleteFlag { get; set; }

    public virtual TblProduct ProductCodeNavigation { get; set; } = null!;

    public virtual TblSale SaleCodeNavigation { get; set; } = null!;
}
