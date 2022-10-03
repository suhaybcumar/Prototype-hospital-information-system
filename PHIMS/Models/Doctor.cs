using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PHIMS.Models
{
    public class Doctor
    {
        [Key]
        public int Doctor_Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
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
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }
        [Required]
        [Display(Name = "Registration Date")]
        public DateTime R_Date { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Specializations { get; set; }
        [Display(Name ="Working Days")]
        public string Working_days { get; set; }
        [Display(Name ="Working Hours")]
        public string Working_hours { get; set; }
        [Display(Name ="National ID")]
        public int National_Id { get; set; }
        [Display(Name ="Empolyee ID")]
        public string Employee_Id { get; set; }
        public string Department { get; set; }
        public bool Available { get; set; }
        [ForeignKey("Admin")]
        [Display(Name ="Admin ID")]
        public int Admin_Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public Admin Admin { get; set; }    
        public  ApplicationUser ApplicationUser { get; set; }
    }
}
