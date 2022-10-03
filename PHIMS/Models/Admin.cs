using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIMS.Models
{
    public class Admin
    {
        [Key]
        public int Admin_Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="First Name")]
        public string F_Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Middle Name")]
        public string M_Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string L_Name { get; set; }
        [Required]
        [Display(Name = "Phone No")]
        public int Phone_Number { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }
        [Required]
        [Display(Name = "Registration Date")]
        public DateTime R_Date { get; set; }
        [Required]
        public string Gender { get; set; }

        [NotMapped]
        public string type { get; set; }

        [NotMapped]
        public int id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }    
    }
}
