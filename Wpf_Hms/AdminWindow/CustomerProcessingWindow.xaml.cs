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
    /// Interaction logic for CustomerProcessingWindow.xaml
    /// </summary>
    public partial class CustomerProcessingWindow : Window
    {
        private readonly IService<Customer> _CustomerService;

        private int newCustomerId;

        private bool isCreateAction;

        private DataGrid dgCustomer;

        public CustomerProcessingWindow(bool isCreateAction, Customer customer, DataGrid dgCustomer)
        {
            InitializeComponent();
            _CustomerService = CustomerService.GetInstance();
            this.isCreateAction = isCreateAction;
            this.dgCustomer = dgCustomer;
            SetDefaultData(customer);
        }

        private void SetDefaultData(Customer customer)
        {
            LoadCustomerStatus();

            newCustomerId = _CustomerService.GetNewId();

            if (isCreateAction)
            {
                lbTitle.Content = "Create Customer";
                txtCustomerId.Text = newCustomerId.ToString();
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
                txtPassword.Text = customer.Password;

                if(customer.CustomerStatus == 2) cbxCustomerStatus.IsEnabled = true;
                else cbxCustomerStatus.IsEnabled = false;
            }
        }

        private void LoadCustomerStatus()
        {
            var customerStatus = ServiceCommon.GetCustomerStatusName();

            cbxCustomerStatus.ItemsSource = customerStatus;
            cbxCustomerStatus.SelectedValuePath = "CustomerStatusId";
            cbxCustomerStatus.DisplayMemberPath = "CustomerStatusName";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new()
            {
                CustomerFullName = txtCustomerFullName.Text ?? string.Empty,
                Telephone = txtTelephone.Text ?? string.Empty,
                EmailAddress = txtEmailAddress.Text ?? string.Empty,
                CustomerBirthday = dpCustomerBirthday.SelectedDate != null ? DateOnly.FromDateTime(dpCustomerBirthday.SelectedDate.Value) : null,
                CustomerStatus = cbxCustomerStatus.SelectedValue != null ? byte.Parse(cbxCustomerStatus.SelectedValue.ToString()!) : null,
                Password = txtPassword.Text ?? string.Empty,
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
                if (isCreateAction)
                {
                    bool addResult = _CustomerService.Add(customer);

                    if (addResult)
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
                    bool updateResult = _CustomerService.Update(customer);

                    if (updateResult)
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
