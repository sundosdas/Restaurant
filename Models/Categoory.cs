using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Categoory
{
    public decimal Id { get; set; }

    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
