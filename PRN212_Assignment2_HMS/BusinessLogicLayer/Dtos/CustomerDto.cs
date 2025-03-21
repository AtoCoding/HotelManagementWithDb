using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string? CustomerFullName { get; set; }

        [Required(ErrorMessage = "Telephone is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telephone must be 10 digits.")]
        public string? Telephone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Birthday is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateOnly? CustomerBirthday { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public byte? CustomerStatus { get; set; }

        public ICollection<BookingReservation> BookingReservations { get; set; } = new List<BookingReservation>();
    }
}
