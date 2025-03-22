using System.Windows;
using BusinessLogicLayer;
using BusinessLogicLayer.Bases;
using BusinessLogicLayer.Dtos;
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

        private CustomerDto customerDto;

        public CustomerDetailsWindow(CustomerDto customerDto)
        {
            InitializeComponent();
            _BookingReservationService = BookingReservationService.GetInstance();
            this.customerDto = customerDto;
            LoadData();
        }

        private void LoadData()
        {
            LoadCustomerInformation();

            List<BookingReservation> bookingReservation = _BookingReservationService.GetList(customerDto.CustomerId);

            if (bookingReservation.Count > 0)
            {
                LoadBookingReservationId(bookingReservation);
            }
        }

        private void LoadCustomerInformation()
        {
            LoadCustomerStatus();

            txtCustomerId.Text = customerDto.CustomerId.ToString();
            txtCustomerFullName.Text = customerDto.CustomerFullName;
            txtTelephone.Text = customerDto.Telephone;
            txtEmailAddress.Text = customerDto.EmailAddress;
            dpCustomerBirthday.SelectedDate = customerDto.CustomerBirthday!.Value.ToDateTime(new TimeOnly(0, 0));
            cbxCustomerStatus.SelectedValue = (int)customerDto.CustomerStatus!;
            txtPassword.Text = customerDto.Password;
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
