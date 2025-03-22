using System.Windows;
using Wpf_Hms.Admin;
using Wpf_Hms.AdminWindow;
using Wpf_Hms.CustomerProfile;

namespace Wpf_Hms
{
    /// <summary>
    /// Interaction logic for OptionWindow.xaml
    /// </summary>
    public partial class OptionWindow : Window
    {
        /// <summary>
        /// Checking window for admin or customer
        /// </summary>
        private readonly bool isAdmin = false;

        private readonly string email = string.Empty;

        public OptionWindow(bool isAdmin, string email)
        {
            this.isAdmin = isAdmin;
            this.email = email;
            InitializeComponent();
            Canvas_Loaded();
        }

        private void Canvas_Loaded()
        {
            if (isAdmin)
            {
                AdminSession.Visibility = Visibility.Visible;
                CustomerSession.Visibility = Visibility.Hidden;
            } 
            else
            {
                AdminSession.Visibility = Visibility.Hidden;
                CustomerSession.Visibility = Visibility.Visible;
            }
        }

        private void BtnCustomerManagement_Click(object sender, RoutedEventArgs e)
        {
            CustomerInformationWindow customerInformationWindow = new CustomerInformationWindow(email);
            customerInformationWindow.Show();
            this.Close();
        }

        private void BtnBooking_Click(object sender, RoutedEventArgs e)
        {
            BookingReservationWindow bookingReservationWindow = new BookingReservationWindow(email);
            bookingReservationWindow.Show();
            this.Close();
        }

        private void BtnRoomManagement_Click(object sender, RoutedEventArgs e)
        {
            RoomInformationWindow roomInformationWindow = new RoomInformationWindow(email);
            roomInformationWindow.Show();
            this.Close();
        }
        
        private void BtnReportManagement_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Comming soon . . .");
        }

        private void BtnBookingHistory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Comming soon . . .");
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            CustomerProfileWindow customerProfileWindow = new CustomerProfileWindow(email);
            customerProfileWindow.Show();
            this.Close();
        }
    }
}
