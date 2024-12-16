using System;
using System.Collections.Generic;

namespace DotNetTrainingBatch5.PointOfSale.Database.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public string VoucherNo { get; set; } = null!;

    public DateTime? SaleDate { get; set; }

    public decimal TotalAmount { get; set; }
}
