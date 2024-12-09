using System;
using System.Collections.Generic;

namespace DotNetTrainingBatch5.PointOfSale.DataBase.Models;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public string ProductCategoryCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool? DeleteFlag { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
