using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Models.DB;

[Index("UserID", Name = "IX_Orders_UserID")]
public partial class Order
{
    [Key]
    public int OrderID { get; set; }

    public int? UserID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OrderDate { get; set; }

    [Column(TypeName = "text")]
    public string? ShippingAddress { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalAmount { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Status { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Order")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("UserID")]
    [InverseProperty("Orders")]
    public virtual User? User { get; set; }
}
