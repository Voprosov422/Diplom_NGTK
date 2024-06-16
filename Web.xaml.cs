using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace PredDeplomohka
{
    public partial class Web : Window
    {
        public Web()
        {
            InitializeComponent();
            InitializeWebView();
        }
        private async void InitializeWebView()
        {
            await WebViewControl.EnsureCoreWebView2Async(null);
            WebViewControl.CoreWebView2.Navigate("https://outlook.office.com/mail/");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (WebViewControl.CanGoBack)
            {
                WebViewControl.GoBack();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this .Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WebViewControl.CoreWebView2.Navigate("https://outlook.office.com/mail/");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (WebViewControl.CanGoForward)
            {
                WebViewControl.GoForward();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
