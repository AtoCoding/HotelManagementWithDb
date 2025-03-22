using System.Windows;
using BusinessLogicLayer;
using BusinessLogicLayer.Bases;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using static BusinessLogicLayer.ServiceCommon;

namespace Wpf_Hms.AdminWindow
{
    /// <summary>
    /// Interaction logic for RoomDetailsWindow.xaml
    /// </summary>
    public partial class RoomDetailsWindow : Window
    {
        private readonly IService<RoomInformation> _RoomInformationService;

        private readonly IService<RoomType> _RoomTypeService;

        private RoomInformation roomInformation;

        public RoomDetailsWindow(RoomInformation roomInformation)
        {
            InitializeComponent();
            _RoomInformationService = RoomInformationService.GetInstance();
            _RoomTypeService = RoomTypeService.GetInstance();
            this.roomInformation = roomInformation;
            LoadRoomInformation();
        }

        private void LoadRoomInformation()
        {
            LoadRoomStatus();
            LoadRoomType();

            txtRoomId.Text = roomInformation.RoomId.ToString();
            txtRoomNumber.Text = roomInformation.RoomNumber;
            txtPrice.Text = roomInformation.RoomPricePerDay.ToString();
            txtCapacity.Text = roomInformation.RoomMaxCapacity.ToString();
            txtRoomDescription.Text = roomInformation.RoomDetailDescription;
            cbxStatus.SelectedValue = (int)roomInformation.RoomStatus!;
            cbxRoomType.SelectedValue = roomInformation.RoomTypeId!;
            txtRoomTypeId.Text = roomInformation.RoomTypeId.ToString();
            txtRoomTypeName.Text = roomInformation.RoomType.RoomTypeName;
            txtRoomTypeDescription.Text = roomInformation.RoomType.TypeDescription;
            txtRoomTypeNote.Text = roomInformation.RoomType.TypeNote;
        }

        private void LoadRoomStatus()
        {
            var roomStatus = ServiceCommon.GetRoomStatusName();

            cbxStatus.ItemsSource = roomStatus;
            cbxStatus.SelectedValuePath = "RoomStatusId";
            cbxStatus.DisplayMemberPath = "RoomStatusName";
        }

        private void LoadRoomType()
        {
            List<RoomType> roomTypes = _RoomTypeService.GetAll();
            cbxRoomType.ItemsSource = roomTypes;
            cbxRoomType.SelectedValuePath = "RoomTypeId";
            cbxRoomType.DisplayMemberPath = "RoomTypeName";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
