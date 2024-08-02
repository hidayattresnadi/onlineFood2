using System.ComponentModel.DataAnnotations;

namespace OnlineFoodWebAPI.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters long.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email format is not valid")]
        public string Email { get; set; }
        [Phone(ErrorMessage ="Phone format tidak valid")]
        public string? PhoneNumber { get; set; }
        [MaxLength(200,ErrorMessage = "maksimal alamat hanya 200 karakter")]
        public string? Address { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
