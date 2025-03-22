using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Bases;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using BusinessLogicLayer;
using DataAccessLayer.Entities;

namespace Wpf_Hms.AdminWindow
{
    /// <summary>
    /// Interaction logic for BookingReservationProcessingWindow.xaml
    /// </summary>
    public partial class BookingReservationProcessingWindow : Window
    {
        private readonly IService<BookingReservation> _BookingReservationService;
        private readonly IService<Customer> _CustomerService;
        private readonly IService<RoomInformation> _RoomInformationService;

        private int newBookingId;

        private List<RoomInformation> _rooms;

        private int daysDiff = 0;

        private DataGrid dgBooking;

        public BookingReservationProcessingWindow(DataGrid dgBooking)
        {
            InitializeComponent();
            _BookingReservationService = BookingReservationService.GetInstance();
            _CustomerService = CustomerService.GetInstance();
            _RoomInformationService = RoomInformationService.GetInstance();
            this.dgBooking = dgBooking;
            _rooms = new();
            SetDefaultData();
        }

        private void SetDefaultData()
        {
            LoadCustomerId();
            LoadRoomId();
            LoadBookingStatus();

            newBookingId = _BookingReservationService.GetNewId();
            dpBookingDate.SelectedDate = DateTime.Now;
            cbxCustomerStatus.SelectedValue = 1;

            txtBookingId.Text = newBookingId.ToString();
        }

        private void LoadCustomerId()
        {
            var customers = _CustomerService.GetAll();

            cbxCustomerId.ItemsSource = customers;
            cbxCustomerId.SelectedValuePath = "CustomerId";
            cbxCustomerId.DisplayMemberPath = "CustomerId";
        }

        private void LoadRoomId()
        {
            var rooms = _RoomInformationService.GetAll();

            lbRoomId.ItemsSource = rooms;
            lbRoomId.DisplayMemberPath = "RoomId";
        }

        private void LoadBookingStatus()
        {
            var bookingStatus = ServiceCommon.GetBookingStatusName();

            cbxCustomerStatus.ItemsSource = bookingStatus;
            cbxCustomerStatus.SelectedValuePath = "BookingStatusId";
            cbxCustomerStatus.DisplayMemberPath = "BookingStatusName";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<BookingDetail> bookingDetails = new();
            foreach (RoomInformation room in _rooms)
            {
                bookingDetails.Add(new()
                {
                    BookingReservationId = int.Parse(txtBookingId.Text),
                    RoomId = room.RoomId,
                    StartDate = dpStartDate.SelectedDate != null ? DateOnly.FromDateTime(dpStartDate.SelectedDate.Value) : DateOnly.FromDateTime(DateTime.Today),
                    EndDate = dpEndDate.SelectedDate != null ? DateOnly.FromDateTime(dpEndDate.SelectedDate.Value) : DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    ActualPrice = room.RoomPricePerDay
                });
            }

            var bookingDetailsValidationResults = new List<ValidationResult>();
            var bookingDetailsContext = new ValidationContext(bookingDetails);

            bool isBookingDetailsValid = Validator.TryValidateObject(bookingDetails, bookingDetailsContext, bookingDetailsValidationResults, true);

            decimal price = 0;
            BookingReservation bookingReservation = new()
            {
                BookingReservationId = int.Parse(txtBookingId.Text),
                BookingDate = dpBookingDate.SelectedDate != null ? DateOnly.FromDateTime(dpBookingDate.SelectedDate.Value) : DateOnly.FromDateTime(DateTime.Today),
                TotalPrice = decimal.TryParse(txtTotalPrice.Text, out price) ? price : null,
                CustomerId = int.Parse(cbxCustomerId.SelectedValue.ToString()!),
                BookingStatus = 1,
                BookingDetails = bookingDetails
            };

            var bookingValidationResults = new List<ValidationResult>();
            var bookingContext = new ValidationContext(bookingReservation);

            bool isBookingValid = Validator.TryValidateObject(bookingReservation, bookingContext, bookingValidationResults, true);

            if (!isBookingDetailsValid)
            {
                string errorMessages = string.Join("\n", bookingDetailsValidationResults.Select(e => e.ErrorMessage));
                MessageBox.Show(errorMessages, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!isBookingValid)
            {
                string errorMessages = string.Join("\n", bookingValidationResults.Select(e => e.ErrorMessage));
                MessageBox.Show(errorMessages, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                bool addResult = _BookingReservationService.Add(bookingReservation);

                if (addResult)
                {
                    MessageBox.Show("Create successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There is an error!");
                }

                dgBooking.ItemsSource = _BookingReservationService.GetAll();
                dgBooking.Items.Refresh();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lbRoomId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (RoomInformation addedRoom in e.AddedItems)
            {
                if (!_rooms.Contains(addedRoom))
                {
                    _rooms.Add(addedRoom);
                }
            }

            foreach (RoomInformation removedRoom in e.RemovedItems)
            {
                _rooms.Remove(removedRoom);
            }

            ProcessTotalPrice(daysDiff);
        }

        private void dpEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStartDate.SelectedDate is DateTime startDate && dpEndDate.SelectedDate is DateTime endDate)
            {
                if (endDate < startDate)
                {
                    MessageBox.Show("Start date cannot after end date");
                    dpEndDate.SelectedDate = null;
                    return;
                }

                daysDiff = (endDate - startDate).Days;

                ProcessTotalPrice(daysDiff);
            }
        }

        private void ProcessTotalPrice(int daysDiff)
        {
            decimal? totalPrice = 0;
            foreach (RoomInformation room in _rooms)
            {
                totalPrice += room.RoomPricePerDay * daysDiff;
            }
            txtTotalPrice.Text = totalPrice.ToString();
        }
    }
}
