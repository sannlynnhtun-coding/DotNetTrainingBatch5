using System;
using System.Collections.Generic;

namespace DotNetTrainingBatch5.PointOfSale.DataBase.Models;

public partial class SaleDetail
{
    public int SaleDetailId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public decimal? Price { get; set; }
}
