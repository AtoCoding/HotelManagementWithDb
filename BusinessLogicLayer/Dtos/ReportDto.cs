namespace BusinessLogicLayer.Dtos
{
    public class ReportDto
    {
        public int BookingReservationId { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
