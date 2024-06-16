using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Logging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PredDeplomohka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string LoginUser = Login.Text;
            string PasswordUser = Pas.Password;
            string role = GetRole(LoginUser, PasswordUser);
            if (role != null)
            {
                MessageBox.Show("Успешно");
                if (role == "User")
                {
                    new WorkWindow().Show();
                    this.Close();
                }
                else
                {
                    new WorkWindow().Show();
                    this.Close();
                }
            }

            else
            {
                MessageBoxResult Error = MessageBox.Show("Невозможно войти в аккаунт! Обратитесь к Кириллу Алексеевичу в 105КБ", "Неправильный логин или пароль! Обратитесь к Кириллу Алексеевичу в 105КБ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private string GetRole(string LoginUser, string PasswordUser)
        {
            string connectionString = "Data Source=B660\\OSK; Initial Catalog=Neptun; Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Role FROM Users WHERE Login = @Login AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Login", LoginUser);
                        command.Parameters.AddWithValue("@Password", PasswordUser);

                        object result = command.ExecuteScalar();

                        return result as string;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return null;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new info().Show();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}