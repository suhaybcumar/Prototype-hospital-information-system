using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIMS.Models
{
    public class Lab
    {
        [Key]
        public Guid Lab_Id { get; set; }
        public string Prescription { get; set; }
        public string Description { get; set; }
        [ForeignKey("Doctor")]
        [Required]
        public int Doctor_Id { get; set; }
        [ForeignKey("Pateint")]
        [Required]
        public int Pateint_Id { get; set; }
        public Doctor Doctor { get; set; }
        public Pateint Pateint { get; set; }
    }
}
