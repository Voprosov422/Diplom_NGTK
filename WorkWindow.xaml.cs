using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;
using System.Net.Http;
using System.Threading.Tasks;

namespace PredDeplomohka
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private const string botToken = "7267101020:AAGGlj2eJSrCDAz0tvAIBQREzVr9ROQjlMw";
        private const string chatId = "458382355";
        private const string TargetDirectory = @"D:\Сервер подключены по сети";
        private readonly string[] allowedExtensions = { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".pdf" };
        public WorkWindow()
        {
            InitializeComponent();
            InitializeListBoxes(); 
        }


        private void InitializeListBoxes()
        {
            
        }


        private void SourceListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void SourceListBox_Drop(object sender, DragEventArgs e)
        {
            
        }

        private void TargetListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void TargetListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    string fileExtension = Path.GetExtension(file).ToLower();
                    if (allowedExtensions.Contains(fileExtension))
                    {
                        try
                        {
                            string fileName = Path.GetFileName(file);
                            string destinationPath = Path.Combine(TargetDirectory, fileName);
                            File.Copy(file, destinationPath, true);
                            MessageBox.Show($"Файл '{fileName}' успешно скопирован в '{TargetDirectory}'.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при копировании файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Файл '{file}' имеет недопустимое расширение и не был скопирован.", "Недопустимое расширение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
            string folderPath = @"D:\Сервер подключены по сети";

            
            Process.Start("explorer.exe", folderPath);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=B660\OSK;Initial Catalog=Neptun;Integrated Security=True;TrustServerCertificate=True");
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            string Get_Data = "SELECT * FROM dbo.Zadaniya";
            SqlCommand cmd = new SqlCommand(Get_Data, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Zadaniya");
            sda.Fill(dt);
            Furry.ItemsSource = dt.DefaultView;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Word и PDF файлы (*.docx, *.pdf)|*.docx;*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                string savePath = @"D:\Сервер подключены по сети";

                try
                {
                   
                    string fileName = System.IO.Path.GetFileName(filePath);
                    string destinationPath = System.IO.Path.Combine(savePath, fileName);
                    System.IO.File.Copy(filePath, destinationPath, true);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузки файла на сервер: " + ex.Message);
                }
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            new Web().Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=B660\\OSK; Initial Catalog=Neptun; Integrated Security=True";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    foreach (var item in Furry.SelectedItems)
                    {
                        DataRowView rowView = (DataRowView)item;
                        DataRow row = rowView.Row;
                        string sqlFactory = $"DELETE FROM Zadaniya WHERE Id = @ID";
                        using (SqlCommand command = new SqlCommand(sqlFactory, sqlConnection))
                        {
                            command.Parameters.Add("@ID", SqlDbType.Int).Value = row["Id"];
                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Готово!");
                    }
                    string Get_Data = $"SELECT * FROM Zadaniya";
                    SqlCommand cmd2 = new SqlCommand(Get_Data, sqlConnection);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                    DataTable dt = new DataTable($"Zadaniya");
                    sda.Fill(dt);
                    Furry.ItemsSource = dt.DefaultView;
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Web().Show();
        }

        private async void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string message = "Пользователь Ольга Волга просит помощь в кабинете 101.";
            await SendMessageToTelegram(message);
        }

        private async Task SendMessageToTelegram(string message)
        {
            string url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={message}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                MessageBox.Show("Сообщение отправлено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка отправки сообщения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}