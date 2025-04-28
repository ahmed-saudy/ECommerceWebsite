using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Models.DB;

public partial class Cart
{
    [Key]
    public int CartID { get; set; }

    public int? UserID { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("UserID")]
    [InverseProperty("Cart")]
    public virtual User? User { get; set; }
}
