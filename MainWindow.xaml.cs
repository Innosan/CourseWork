using System;
using System.Windows;
using System.Windows.Media;

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

            passBox.GotFocus += new RoutedEventHandler(passBox_Focus);

            passBox.Foreground = Brushes.Gray;
        }

        private void passBox_Focus(object sender, EventArgs e)
        {
            passBox.Password = "";
            passBox.Foreground = Brushes.White;
        }

        private void toSignUpHypLink_Click (object sender, EventArgs e)
        {
            var newForm = new RegistrationWindow();

            newForm.Show();
            this.Close();
        }
    }
}
