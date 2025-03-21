using System.Windows;
using BusinessLogicLayer.Services;


namespace Wpf_Hms
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly CustomerService _CustomerService;

        public LoginWindow()
        {
            InitializeComponent();
            _CustomerService = CustomerService.GetInstance();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            (bool isAuthen, string role) = _CustomerService.CheckAuth(email, password);

            if (isAuthen)
            {
                switch (role)
                {
                    case "Admin":
                        OptionWindow adminOptionWindow = new OptionWindow(true, email);                        
                        adminOptionWindow.Show();
                        this.Close();
                        break;
                    case "Customer":
                        OptionWindow customerOptionWindow = new OptionWindow(false, email);
                        customerOptionWindow.Show();
                        this.Close();
                        break;
                    default:
                        break;
                }
            } 
            else
            {
                MessageBox.Show("Invalid email or password");
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
