using System.Windows;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Bases;
using DataAccessLayer.Entities;
using BusinessLogicLayer.Dtos;
namespace Wpf_Hms.AdminWindow
{
    /// <summary>
    /// Interaction logic for ReportStatisticWindow.xaml
    /// </summary>
    public partial class ReportStatisticWindow : Window
    {
        private readonly ReportService _ReportService;

        private readonly string email = string.Empty;

        public ReportStatisticWindow(string email)
        {
            InitializeComponent();
            this.email = email;
            txtWelcomeMessage.Content = $"Hello, {email}";
            _ReportService = ReportService.GetInstance();
            LoadAllReport();
        }

        private void LoadAllReport()
        {
            var reports = _ReportService.GetAll();
            decimal? totalPrice = 0;

            txtCount.Text = reports.Count.ToString();
            foreach (ReportDto reportDto in reports)
            {
                DateTime start = reportDto.StartDate.ToDateTime(new TimeOnly(0, 0));
                DateTime end = reportDto.EndDate.ToDateTime(new TimeOnly(0, 0));
                totalPrice += reportDto.TotalPrice * (end - start).Days;
            }
            txtTotalPrice.Text = totalPrice.ToString();

            LoadDataWindow(reports);
        }

        private void LoadDataWindow(List<ReportDto> reports)
        {
            dgReport.ItemsSource = reports;
            dgReport.Items.Refresh();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OptionWindow optionWindow = new(true, email);
            optionWindow.Show();
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //int customerId = 0, tmpCustomerId = 0;
            //customerId = int.TryParse(txtCustomerIdSearch.Text.ToString(), out tmpCustomerId) ? tmpCustomerId : 0;

            //var customers = _BookingReservationService.Search(customerId);

            //LoadDataWindow(customers);
        }
    }
}