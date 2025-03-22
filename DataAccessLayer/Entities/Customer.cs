using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public partial class Customer
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

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Password must be between 3 and 20 characters.")]
    public string? Password { get; set; }

    public virtual ICollection<BookingReservation> BookingReservations { get; set; } = new List<BookingReservation>();
}
