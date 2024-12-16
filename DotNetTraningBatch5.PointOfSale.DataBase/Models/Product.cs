using System;
using System.Collections.Generic;

namespace DotNetTrainingBatch5.PointOfSale.DataBase.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string ProductCategoryCode { get; set; } = null!;

    public bool? DeleteFlag { get; set; }

    public virtual ProductCategory ProductCategoryCodeNavigation { get; set; } = null!;
}
