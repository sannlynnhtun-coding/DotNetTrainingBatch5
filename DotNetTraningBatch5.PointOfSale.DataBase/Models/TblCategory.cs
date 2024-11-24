using System;
using System.Collections.Generic;

namespace DotNetTrainingBatch5.PointOfSale.DataBase.Models;

public partial class TblCategory
{
    public int CategoryId { get; set; }

    public string CategoryCode { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public bool DeleteFlag { get; set; }

    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
