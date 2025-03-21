using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public partial class BookingReservation
{
    public int BookingReservationId { get; set; }

    [Required(ErrorMessage = "Booking's date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
    public DateOnly? BookingDate { get; set; }

    [Required(ErrorMessage = "Price($) is not a number.")]
    [Range(0, 10000, ErrorMessage = "Price($) must be between 0 and 10,000.")]
    public decimal? TotalPrice { get; set; }

    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public byte? BookingStatus { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Customer Customer { get; set; } = null!;
}
