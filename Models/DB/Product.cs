using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Models.DB;

[Index("CategoryID", Name = "IX_Products_CategoryID")]
public partial class Product
{
    [Key]
    public int ProductID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; } = null!;


    [StringLength(50)]
    public string? InSection { get; set; }

    public string? ShortDescription { get; set; }

    public string? LongDescription { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    public int? StockQuantity { get; set; }

    public int? CategoryID { get; set; }

    [StringLength(255)]
    public string? ImageName { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("CategoryID")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [InverseProperty("Product")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
