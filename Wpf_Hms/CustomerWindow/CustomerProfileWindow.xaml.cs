using System.ComponentModel.DataAnnotations;
using System.Windows;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Bases;
using DataAccessLayer.Entities;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using BusinessLogicLayer.Constant;
using BusinessLogicLayer;

namespace Wpf_Hms.CustomerWindow
{
    /// <summary>
    /// Interaction logic for CustomerProfileWindow.xaml
    /// </summary>
    public partial class CustomerProfileWindow : Window
    {
        private readonly IService<Customer> _CustomerService;

        private readonly string email = string.Empty;

        public CustomerProfileWindow(string email)
        {
            InitializeComponent();
            this.email = email;
            _CustomerService = CustomerService.GetInstance();
            SetDefaultData();
        }

        private void SetDefaultData()
        {
            LoadCustomerStatus();

            Customer customer = _CustomerService.GetAll().FirstOrDefault(x => x.EmailAddress == email)!;

            txtCustomerId.Text = customer.CustomerId.ToString();
            txtCustomerFullName.Text = customer.CustomerFullName;
            txtTelephone.Text = customer.Telephone;
            txtEmailAddress.Text = customer.EmailAddress;
            dpCustomerBirthday.SelectedDate = customer.CustomerBirthday!.Value.ToDateTime(new TimeOnly(0, 0));
            cbxCustomerStatus.SelectedValue = (int)customer.CustomerStatus!;
            txtPassword.Text = customer.Password;
        }

        private void LoadCustomerStatus()
        {
            var customerStatus = ServiceCommon.GetCustomerStatusName();

            cbxCustomerStatus.ItemsSource = customerStatus;
            cbxCustomerStatus.SelectedValuePath = "CustomerStatusId";
            cbxCustomerStatus.DisplayMemberPath = "CustomerStatusName";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            txtCustomerFullName.IsEnabled = true;
            txtTelephone.IsEnabled = true;
            txtPassword.IsEnabled = true;
            dpCustomerBirthday.IsEnabled = true;

            btnSave.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Hidden;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new()
            {
                CustomerId = int.Parse(txtCustomerId.Text),
                CustomerFullName = txtCustomerFullName.Text ?? string.Empty,
                Telephone = txtTelephone.Text ?? string.Empty,
                EmailAddress = txtEmailAddress.Text ?? string.Empty,
                CustomerBirthday = dpCustomerBirthday.SelectedDate != null ? DateOnly.FromDateTime(dpCustomerBirthday.SelectedDate.Value) : null,
                CustomerStatus = cbxCustomerStatus.SelectedValue != null ? byte.Parse(cbxCustomerStatus.SelectedValue.ToString()!) : null,
                Password = txtPassword.Text
            };

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(customer);

            bool isValid = Validator.TryValidateObject(customer, context, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join("\n", validationResults.Select(e => e.ErrorMessage));
                MessageBox.Show(errorMessages, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                bool dataUpdate = _CustomerService.Update(customer);

                if (dataUpdate)
                {
                    MessageBox.Show("Update successfully!");
                }
                else
                {
                    MessageBox.Show("There is an error!");
                }

                txtCustomerFullName.IsEnabled = false;
                txtTelephone.IsEnabled = false;
                txtPassword.IsEnabled = false;
                dpCustomerBirthday.IsEnabled = false;

                btnUpdate.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Hidden;

                SetDefaultData();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OptionWindow optionWindow = new(false, email);
            optionWindow.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
