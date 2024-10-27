using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Windows.UI;

namespace LinkedElementByID.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var _ = new Microsoft.Xaml.Behaviors.DefaultTriggerAttribute(typeof(Trigger), typeof(Microsoft.Xaml.Behaviors.TriggerBase), null);
            MainWindowVM VM = new MainWindowVM();
            this.DataContext = VM;  
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://instudio-engineers.com/");
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string email = $"info@instudio-engineers.com";
            Process.Start(new ProcessStartInfo("mailto:" + email));
            e.Handled = true;
        }
        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed && e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else { WindowState = WindowState.Maximized; }
        }
    }
}
