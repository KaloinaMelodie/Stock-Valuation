using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models
{
    public class Method
    {
        [Key]
        public int IdMethod { get; set; }

        [Required]
        [StringLength(90)]
        public string? MethodName { get; set; }
    }
}
