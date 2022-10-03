using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PHIMS.Models
{
    public class Pateint
    {
        [Key]
        public int Pateint_Id { get; set; }
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
        [ForeignKey("User")]
        [Required]
        [Display(Name ="User ID")]
        public int User_Id { get; set; }
        [ForeignKey("Doctor")]
        [Required]
        [Display(Name ="Doctor ID")]
        public int Doctr_Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Fee { get; set; }
        public int MyProperty { get; set; }
        [Display(Name ="Visit")]
        [Required]
        [DataType(DataType.Text)]
        public string Type { get; set; }
        public bool Approve { get; set; }
        public string State { get; set; }
       
        //foreing refrences
        public Doctor Doctor { get; set; }
        public User User { get; set; }

    }
}
