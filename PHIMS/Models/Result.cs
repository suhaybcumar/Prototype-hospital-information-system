using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHIMS.Models
{
    public class Result
    {
        [Key]
        public int Id_Result { get; set; }

        public string result { get; set; }
        public string Advice { get; set; }
        [Required]
        [ForeignKey("Pateint")]
        public int Pateint_ID { get; set; }
        [Required]
        [ForeignKey("Doctor")]
        public int Doctor_ID { get; set; }
        public Doctor Doctor { get; set; }
        public Pateint Pateint { get; set; }
    }
}
