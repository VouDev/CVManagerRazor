using System.ComponentModel.DataAnnotations;

namespace CVManagerRazor.Models
{
    public class Entry
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string? FirstName { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string? LastName { get; set; }

        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be a 10-digit number")]
        public string? Mobile { get; set; }
        public string? Degree { get; set; }

        [Display(Name = "CV File")]
        public string? File { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
