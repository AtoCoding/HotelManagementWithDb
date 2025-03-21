﻿using System.Windows;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Bases;
using DataAccessLayer.Entities;
namespace Wpf_Hms.AdminWindow
{
    /// <summary>
    /// Interaction logic for CustomerInformationWindow.xaml
    /// </summary>
    public partial class CustomerInformationWindow : Window
    {
        private readonly IService<Customer> _CustomerService;

        private readonly string email = string.Empty;

        public CustomerInformationWindow(string email)
        {
            InitializeComponent();
            this.email = email;
            txtWelcomeMessage.Content = $"Hello, {email}";
            _CustomerService = CustomerService.GetInstance();
            LoadCustomerInformation();
        }

        private void LoadCustomerInformation()
        {
            var customers = _CustomerService.GetAll();

            LoadDataWindow(customers);
        }

        private void LoadDataWindow(List<Customer> customers)
        {
            dgCustomer.ItemsSource = customers;
            dgCustomer.Items.Refresh();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CustomerProcessingWindow customerProcessingWindow = new(true, null!, dgCustomer);
            customerProcessingWindow.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = (Customer)dgCustomer.SelectedItem;
            if (customer == null)
            {
                MessageBox.Show("Please select a room to update");
                return;
            }
            CustomerProcessingWindow customerProcessingWindow = new(false, customer, dgCustomer);
            customerProcessingWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Do you really want to delete this customer?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Customer Customer = (Customer)dgCustomer.SelectedItem;

                if (Customer == null)
                {
                    MessageBox.Show("Please select a room to delete");
                    return;
                }

                if (_CustomerService.Delete(Customer.CustomerId))
                {
                    MessageBox.Show("Delete successfully");
                }
                else
                {
                    MessageBox.Show("Delete failed");
                }
                LoadCustomerInformation();
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
            string? fullnameSearch = txtFullNameSearch.Text;
            string? telephoneSearch = txtTelephoneSearch.Text;
            string? emailAddressSearch = txtEmailAddressSearch.Text;


            var customers = _CustomerService.Search(fullnameSearch, telephoneSearch, emailAddressSearch);

            LoadDataWindow(customers);
        }

        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = (Customer)dgCustomer.SelectedItem;
            if (customer == null)
            {
                MessageBox.Show("Please select a customer to view");
                return;
            }
            CustomerDetailsWindow customerDetailsWindow = new(customer);
            customerDetailsWindow.ShowDialog();
        }
    }
}