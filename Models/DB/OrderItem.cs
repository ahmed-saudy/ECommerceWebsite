using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Models.DB;

[Index("OrderID", Name = "IX_OrderItems_OrderID")]
[Index("ProductID", Name = "IX_OrderItems_ProductID")]
public partial class OrderItem
{
    [Key]
    public int OrderItemID { get; set; }

    public int? OrderID { get; set; }

    public int? ProductID { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? UnitPrice { get; set; }

    [ForeignKey("OrderID")]
    [InverseProperty("OrderItems")]
    public virtual Order? Order { get; set; }

    [ForeignKey("ProductID")]
    [InverseProperty("OrderItems")]
    public virtual Product? Product { get; set; }
}
