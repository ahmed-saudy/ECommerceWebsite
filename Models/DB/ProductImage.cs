using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Models.DB;

[Index("ProductId", Name = "IX_ProductImages_ProductId")]
public partial class ProductImage
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string ImageName { get; set; } = null!;

    public int ProductId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UploadDate { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductImages")]
    public virtual Product Product { get; set; } = null!;
}
