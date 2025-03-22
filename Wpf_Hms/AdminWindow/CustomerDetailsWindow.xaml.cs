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
    public partial class CustomerDetailsWindow : Window
    {
        private readonly IService<BookingReservation> _BookingReservationService;

        private Customer Customer;

        public CustomerDetailsWindow(Customer Customer)
        {
            InitializeComponent();
            _BookingReservationService = BookingReservationService.GetInstance();
            this.Customer = Customer;
            LoadData();
        }

        private void LoadData()
        {
            LoadCustomerInformation();

            List<BookingReservation> bookingReservation = _BookingReservationService.GetList(Customer.CustomerId);

            if (bookingReservation.Count > 0)
            {
                LoadBookingReservationId(bookingReservation);
            }
        }

        private void LoadCustomerInformation()
        {
            LoadCustomerStatus();

            txtCustomerId.Text = Customer.CustomerId.ToString();
            txtCustomerFullName.Text = Customer.CustomerFullName;
            txtTelephone.Text = Customer.Telephone;
            txtEmailAddress.Text = Customer.EmailAddress;
            dpCustomerBirthday.SelectedDate = Customer.CustomerBirthday!.Value.ToDateTime(new TimeOnly(0, 0));
            cbxCustomerStatus.SelectedValue = (int)Customer.CustomerStatus!;
            txtPassword.Text = Customer.Password;
        }

        private void LoadCustomerStatus()
        {
            var customerStatus = ServiceCommon.GetCustomerStatusName();

            cbxCustomerStatus.ItemsSource = customerStatus;
            cbxCustomerStatus.SelectedValuePath = "CustomerStatusId";
            cbxCustomerStatus.DisplayMemberPath = "CustomerStatusName";
        }

        private void LoadBookingReservationId(List<BookingReservation> bookingReservations)
        {
            cbxBookingReservationId.ItemsSource = bookingReservations;
            cbxBookingReservationId.SelectedValuePath = "BookingReservationId";
            cbxBookingReservationId.DisplayMemberPath = "BookingReservationId";
        }

        private void LoadRoomId(List<BookingDetail> bookingDetails)
        {
            cbxRoomId.ItemsSource = bookingDetails;
            cbxRoomId.SelectedValuePath = "RoomId";
            cbxRoomId.DisplayMemberPath = "RoomId";
        }

        private void cbxBookingReservationId_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbxBookingReservationId.SelectedItem is BookingReservation selectedItem)
            {
                dpBookingDate.SelectedDate = selectedItem.BookingDate!.Value.ToDateTime(new TimeOnly(0, 0));
                txtTotalPrice.Text = selectedItem.TotalPrice.ToString();
                LoadRoomId(selectedItem.BookingDetails.ToList());
            }
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
