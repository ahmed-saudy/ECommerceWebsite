using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Models.DB;

[Index("CartID", Name = "IX_CartItems_CartID")]
[Index("ProductID", Name = "IX_CartItems_ProductID")]
public partial class CartItem
{
    [Key]
    public int CartItemID { get; set; }

    public int? CartID { get; set; }

    public int? ProductID { get; set; }

    public int? Quantity { get; set; }

    [ForeignKey("CartID")]
    [InverseProperty("CartItems")]
    public virtual Cart? Cart { get; set; }

    [ForeignKey("ProductID")]
    [InverseProperty("CartItems")]
    public virtual Product? Product { get; set; }
}
