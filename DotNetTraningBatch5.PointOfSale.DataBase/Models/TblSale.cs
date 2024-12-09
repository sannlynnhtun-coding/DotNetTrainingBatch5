using System;
using System.Collections.Generic;

namespace DotNetTrainingBatch5.PointOfSale.DataBase.Models;

public partial class TblSale
{
    public int SaleId { get; set; }

    public string SaleCode { get; set; }

    public DateTime SaleDate { get; set; }

    public decimal TotalSale { get; set; }

    public decimal PayAmount { get; set; }

    public decimal? ChangeAmount { get; set; }

    public bool DeleteFlag { get; set; }

    public virtual ICollection<TblSaleDetail> TblSaleDetails { get; set; } = new List<TblSaleDetail>();
}
