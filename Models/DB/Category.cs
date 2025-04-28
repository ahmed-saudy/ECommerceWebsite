using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Models.DB;

public partial class Category
{
    [Key]
    public int CategoryID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
