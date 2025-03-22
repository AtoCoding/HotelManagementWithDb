using System.Windows;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Bases;
using DataAccessLayer.Entities;
namespace Wpf_Hms.AdminWindow
{
    /// <summary>
    /// Interaction logic for BookingReservationWindow.xaml
    /// </summary>
    public partial class BookingReservationWindow : Window
    {
        private readonly IService<BookingReservation> _BookingReservationService;

        private readonly string email = string.Empty;

        public BookingReservationWindow(string email)
        {
            InitializeComponent();
            this.email = email;
            txtWelcomeMessage.Content = $"Hello, {email}";
            _BookingReservationService = BookingReservationService.GetInstance();
            LoadBookingInformation();
        }

        private void LoadBookingInformation()
        {
            var bookings = _BookingReservationService.GetAll();

            LoadDataWindow(bookings);
        }

        private void LoadDataWindow(List<BookingReservation> bookings)
        {
            dgBooking.ItemsSource = bookings;
            dgBooking.Items.Refresh();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            BookingReservationProcessingWindow bookingReservationProcessingWindow = new(dgBooking);
            bookingReservationProcessingWindow.ShowDialog();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OptionWindow optionWindow = new(true, email);
            optionWindow.Show();
            this.Close();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int customerId = 0, tmpCustomerId = 0;
            customerId = int.TryParse(txtCustomerIdSearch.Text.ToString(), out tmpCustomerId) ? tmpCustomerId : 0;

            var customers = _BookingReservationService.Search(customerId);

            LoadDataWindow(customers);
        }

        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            BookingReservation booking = (BookingReservation)dgBooking.SelectedItem;
            if (booking == null)
            {
                MessageBox.Show("Please select a booking to view");
                return;
            }
            BookingDetailsWindow bookingDetailsWindow = new(booking);
            bookingDetailsWindow.ShowDialog();
        }
    }
}