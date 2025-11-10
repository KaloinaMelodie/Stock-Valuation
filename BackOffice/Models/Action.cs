using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models
{
    public class Action
    {
        [Key]
        public int IdAction { get; set; }

        [Required]
        [StringLength(90)]
        public string ActionName { get; set; }
    }
}
