using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Bases;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using BusinessLogicLayer.Constant;

namespace Wpf_Hms.Admin
{
    /// <summary>
    /// Interaction logic for CustomerProcessingWindow.xaml
    /// </summary>
    public partial class CustomerProcessingWindow : Window
    {
        private readonly IService<CustomerDto> _CustomerService;

        private bool isCreateAction;

        private DataGrid dgCustomer;

        public CustomerProcessingWindow(bool isCreateAction, CustomerDto customer, DataGrid dgCustomer)
        {
            InitializeComponent();
            _CustomerService = CustomerService.GetInstance();
            this.isCreateAction = isCreateAction;
            this.dgCustomer = dgCustomer;
            SetDefaultData(customer);
        }

        private void SetDefaultData(CustomerDto customer)
        {
            LoadCustomerStatus();

            if (isCreateAction)
            {
                lbTitle.Content = "Create Customer";
            }
            else
            {
                lbTitle.Content = "Update Customer";
                txtCustomerId.Text = customer.CustomerId.ToString();
                txtCustomerFullName.Text = customer.CustomerFullName;
                txtTelephone.Text = customer.Telephone;
                txtEmailAddress.Text = customer.EmailAddress;
                dpCustomerBirthday.SelectedDate = customer.CustomerBirthday!.Value.ToDateTime(new TimeOnly(0, 0));
                cbxCustomerStatus.SelectedValue = (int)customer.CustomerStatus!;
            }
        }

        private void LoadCustomerStatus()
        {
            List<(byte? CustomerStatusId, string CustomerRoomStatusName)> customerStatus = _CustomerService.GetAll()
                                                .Select(x =>
                                                {
                                                    string statusStr = x.CustomerStatus switch
                                                    {
                                                        1 => HMSDisplayConst.CUSTOMER_ACTIVE,
                                                        2 => HMSDisplayConst.CUSTOMER_DELETED,
                                                        _ => string.Empty
                                                    };

                                                    return (x.CustomerStatus, statusStr);
                                                }).ToList();

            cbxCustomerStatus.ItemsSource = customerStatus;
            cbxCustomerStatus.SelectedValuePath = "CustomerStatusId";
            cbxCustomerStatus.DisplayMemberPath = "CustomerRoomStatusName";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CustomerDto customerDto = new()
            {
                CustomerId = int.Parse(txtCustomerId.Text),
                CustomerFullName = txtCustomerFullName.Text ?? string.Empty,
                Telephone = txtTelephone.Text ?? string.Empty,
                EmailAddress = txtEmailAddress.Text ?? string.Empty,
                CustomerBirthday = dpCustomerBirthday.SelectedDate != null ? DateOnly.FromDateTime(dpCustomerBirthday.SelectedDate.Value) : null,
                CustomerStatus = cbxCustomerStatus.SelectedValue != null ? (byte)cbxCustomerStatus.SelectedValue : null,
                Password = txtPassword.Text ?? string.Empty,
            };

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(customerDto);

            bool isValid = Validator.TryValidateObject(customerDto, context, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join("\n", validationResults.Select(e => e.ErrorMessage));
                MessageBox.Show(errorMessages, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (isCreateAction)
                {
                    var dataAdd = _CustomerService.Add(customerDto);

                    if (dataAdd != null)
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
                    var dataUpdate = _CustomerService.Update(customerDto);

                    if (dataUpdate != null)
                    {
                        MessageBox.Show("Update successfully!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("There is an error!");
                    }
                }

                dgCustomer.ItemsSource = _CustomerService.GetAll();
                dgCustomer.Items.Refresh();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
