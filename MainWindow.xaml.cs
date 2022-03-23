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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loginBox.GotFocus += new RoutedEventHandler(loginBox_Focus);
            passBox.GotFocus += new RoutedEventHandler(passBox_Focus);
            loginBox.Text = "Логин...";
            loginBox.Foreground = Brushes.Gray;
            passBox.Foreground = Brushes.Gray;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void loginBox_Focus(object sender, EventArgs e)
        {
            loginBox.Text = "";
            loginBox.Foreground = Brushes.White;
        }

        private void passBox_Focus(object sender, EventArgs e)
        {
            passBox.Password = "";
            passBox.Foreground = Brushes.White;
        }
    }
}
