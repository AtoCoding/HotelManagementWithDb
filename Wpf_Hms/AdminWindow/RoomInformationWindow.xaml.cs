using System.Windows;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Bases;
using DataAccessLayer.Entities;
using Wpf_Hms.Admin;

namespace Wpf_Hms.AdminWindow
{
    /// <summary>
    /// Interaction logic for RoomInformationWindow.xaml
    /// </summary>
    public partial class RoomInformationWindow : Window
    {
        private readonly IService<RoomInformation> _RoomInformationService;
       
        private readonly string email = string.Empty;

        public RoomInformationWindow(string email)
        {
            InitializeComponent();
            this.email = email;
            _RoomInformationService = RoomInformationService.GetInstance();
            LoadRoomInformation();
        }

        private void LoadRoomInformation()
        {
            var roomsInfor = _RoomInformationService.GetAll();

            LoadDataWindow(roomsInfor);
        }

        private void LoadDataWindow(List<RoomInformation> roomsInformation)
        {
            dgHotel.ItemsSource = roomsInformation;
            dgHotel.Items.Refresh();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            RoomProcessingWindow roomProcessingWindow = new(true, null!, dgHotel);
            roomProcessingWindow.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation roomInformation = (RoomInformation)dgHotel.SelectedItem;
            if (roomInformation == null)
            {
                MessageBox.Show("Please select a room to update");
                return;
            }
            RoomProcessingWindow roomProcessingWindow = new(false, roomInformation, dgHotel);
            roomProcessingWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Do you really want to delete this room?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                RoomInformation roomInformation = (RoomInformation)dgHotel.SelectedItem;

                if (roomInformation == null)
                {
                    MessageBox.Show("Please select a room to delete");
                    return;
                }

                if (_RoomInformationService.Delete(roomInformation.RoomId))
                {
                    MessageBox.Show("Delete successfully");
                }
                else
                {
                    MessageBox.Show("Delete failed");
                }
                LoadRoomInformation();
            }
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
            string? descSearch = txtDescriptionSearch.Text;
            string? typeNameSearch = txtTypeNameSearch.Text;
            int capacitySearch = 0;
            _ = int.TryParse(txtCapacitySearch.Text, out capacitySearch);

            var roomsInformation = _RoomInformationService.Search(descSearch, typeNameSearch, capacitySearch);

            LoadDataWindow(roomsInformation);
        }
    }
}