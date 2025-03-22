using System.Windows;
using BusinessLogicLayer;
using BusinessLogicLayer.Bases;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using static BusinessLogicLayer.ServiceCommon;

namespace Wpf_Hms.AdminWindow
{
    /// <summary>
    /// Interaction logic for CustomerDetailsWindow.xaml
    /// </summary>
    public partial class BookingDetailsWindow : Window
    {
        private readonly IService<BookingReservation> _BookingReservationService;

        private BookingReservation booking;

        public BookingDetailsWindow(BookingReservation booking)
        {
            InitializeComponent();
            _BookingReservationService = BookingReservationService.GetInstance();
            this.booking = booking;
            LoadBookingInformation();
        }

        private void LoadBookingInformation()
        {
            LoadBookingStatus();
            LoadRoomId(booking.BookingDetails.ToList());

            txtBookingId.Text = booking.BookingReservationId.ToString();
            dpBookingDate.SelectedDate = booking.BookingDate!.Value.ToDateTime(new TimeOnly(0, 0));
            txtTotalPrice.Text = booking.TotalPrice.ToString();
            cbxBookingStatus.SelectedValue = booking.BookingStatus.ToString();
            txtCustomerId.Text = booking.CustomerId.ToString();
            txtCustomerName.Text = booking.Customer.CustomerFullName;
        }

        private void LoadBookingStatus()
        {
            var bookingStatus = ServiceCommon.GetBookingStatusName();

            cbxBookingStatus.ItemsSource = bookingStatus;
            cbxBookingStatus.SelectedValuePath = "BookingStatusId";
            cbxBookingStatus.DisplayMemberPath = "BookingStatusName";
        }

        private void LoadRoomId(List<BookingDetail> bookingDetails)
        {
            cbxRoomId.ItemsSource = bookingDetails;
            cbxRoomId.SelectedValuePath = "RoomId";
            cbxRoomId.DisplayMemberPath = "RoomId";
        }

        private void cbxRoomId_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbxRoomId.SelectedItem is BookingDetail selectedItem)
            {
                dpStartDate.SelectedDate = selectedItem.StartDate.ToDateTime(new TimeOnly(0, 0));
                dpEndDate.SelectedDate = selectedItem.EndDate.ToDateTime(new TimeOnly(0, 0));
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
