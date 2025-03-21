using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public partial class RoomInformation
{
    public int RoomId { get; set; }

    [Required(ErrorMessage = "Room Number is required.")]
    [StringLength(50, ErrorMessage = "Room Number cannot exceed 50 characters.")]
    public string RoomNumber { get; set; } = null!;

    [Required(ErrorMessage = "Room Description is required.")]
    [StringLength(200, ErrorMessage = "Room Description cannot exceed 200 characters.")]
    public string? RoomDetailDescription { get; set; }

    [Required(ErrorMessage = "Max capacity is not a number.")]
    [Range(1, 30, ErrorMessage = "Max capacity must be between 1 and 30.")]
    public int? RoomMaxCapacity { get; set; }

    [Required(ErrorMessage = "Room's type is required.")]
    public int RoomTypeId { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public byte? RoomStatus { get; set; }

    [Required(ErrorMessage = "Price($) is not a number.")]
    [Range(0, 10000, ErrorMessage = "Price($) must be between 0 and 10,000.")]
    public decimal? RoomPricePerDay { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual RoomType RoomType { get; set; } = null!;
}
