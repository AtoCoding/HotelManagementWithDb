using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public partial class BookingDetail
{
    public int BookingReservationId { get; set; }

    public int RoomId { get; set; }

    [Required(ErrorMessage = "Start date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
    public DateOnly StartDate { get; set; }

    [Required(ErrorMessage = "End date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
    public DateOnly EndDate { get; set; }

    [Required(ErrorMessage = "Price($) is not a number.")]
    [Range(0, 10000, ErrorMessage = "Price($) must be between 0 and 10,000.")]
    public decimal? ActualPrice { get; set; }

    public virtual BookingReservation BookingReservation { get; set; } = null!;

    public virtual RoomInformation Room { get; set; } = null!;
}
