using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Bases;
using DataAccessLayer.Entities;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using BusinessLogicLayer;

namespace Wpf_Hms.Admin
{
    /// <summary>
    /// Interaction logic for RoomProcessingWindow.xaml
    /// </summary>
    public partial class RoomProcessingWindow : Window
    {
        private readonly IService<RoomInformation> _RoomInformationService;

        private readonly IService<RoomType> _RoomTypeService;

        private int newRoomId;

        private bool isCreateAction;

        private DataGrid dgHotel;

        public RoomProcessingWindow(bool isCreateAction, RoomInformation roomInformation, DataGrid dgHotel)
        {
            InitializeComponent();
            _RoomInformationService = RoomInformationService.GetInstance();
            _RoomTypeService = RoomTypeService.GetInstance();
            this.isCreateAction = isCreateAction;
            this.dgHotel = dgHotel;
            SetDefaultData(roomInformation);
        }

        private void SetDefaultData(RoomInformation roomInformation)
        {
            LoadRoomStatus();
            LoadRoomType();

            newRoomId = _RoomInformationService.GetNewId();

            if (isCreateAction)
            {
                lbTitle.Content = "Create Hotel Room";
                txtRoomId.Text = newRoomId.ToString();
            }
            else
            {
                lbTitle.Content = "Update Hotel Room";
                txtRoomId.Text = roomInformation.RoomId.ToString();
                txtRoomNumber.Text = roomInformation.RoomNumber?.ToString();
                txtCapacity.Text = roomInformation.RoomMaxCapacity.ToString();
                txtRoomDescription.Text = roomInformation.RoomDetailDescription?.ToString();
                txtPrice.Text = roomInformation.RoomPricePerDay.ToString();
                cbxStatus.SelectedValue = (int)roomInformation.RoomStatus!;
                cbxRoomType.SelectedValue = roomInformation.RoomType?.RoomTypeId;

                if (roomInformation.RoomStatus == 2) cbxStatus.IsEnabled = true;
                else cbxStatus.IsEnabled = false;
            }
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int maxCapacity = 0;
            decimal pricePerDate = 0;
            RoomInformation roomInformation = new()
            {
                RoomId = int.Parse(txtRoomId.Text),
                RoomNumber = txtRoomNumber.Text ?? string.Empty,
                RoomDetailDescription = txtRoomDescription.Text ?? string.Empty,
                RoomMaxCapacity = int.TryParse(txtCapacity.Text, out maxCapacity) ? maxCapacity : null,
                RoomStatus = cbxStatus.SelectedValue != null ? byte.Parse(cbxStatus.SelectedValue.ToString()!) : null,
                RoomPricePerDay = decimal.TryParse(txtPrice.Text, out pricePerDate) ? pricePerDate : null,
                RoomTypeId = cbxRoomType.SelectedValue != null ? int.Parse(cbxRoomType.SelectedValue.ToString()!) : 0
            };

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(roomInformation);

            bool isValid = Validator.TryValidateObject(roomInformation, context, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join("\n", validationResults.Select(e => e.ErrorMessage));
                MessageBox.Show(errorMessages, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (isCreateAction)
                {
                    var dataAdd = _RoomInformationService.Add(roomInformation);

                    if (dataAdd)
                    {
                        MessageBox.Show("Create successfully!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("There is an error!");
                    }
                }
                else
                {
                    var dataUpdate = _RoomInformationService.Update(roomInformation);

                    if (dataUpdate)
                    {
                        MessageBox.Show("Update successfully!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("There is an error!");
                    }
                }

                dgHotel.ItemsSource = _RoomInformationService.GetAll();
                dgHotel.Items.Refresh();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
