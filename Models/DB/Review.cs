using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Models.DB;

[Index("ProductID", Name = "IX_Reviews_ProductID")]
[Index("UserID", Name = "IX_Reviews_UserID")]
public partial class Review
{
    [Key]
    public int ReviewID { get; set; }

    public int? UserID { get; set; }

    public int? ProductID { get; set; }

    public int? Rating { get; set; }

    [Column(TypeName = "text")]
    public string? Comment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReviewDate { get; set; }

    [ForeignKey("ProductID")]
    [InverseProperty("Reviews")]
    public virtual Product? Product { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("Reviews")]
    public virtual User? User { get; set; }
}
