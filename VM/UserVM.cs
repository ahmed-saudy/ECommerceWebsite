using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWebsite.VM
{
    public class UserVM
    {
        public int? UserID { get ; set; }

        [Required]
        public string? UserName { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        //[DataType(DataType.PhoneNumber)]
        [Length(11,11,ErrorMessage ="Please Write Correct Number")]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }
        public string? Image { get; set; }
      

    }
}
