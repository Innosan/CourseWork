using System;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Linq;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string publicUserName;
        public int publicUserRole;

        public MainWindow(string userName, int userRole)
        {
            InitializeComponent();

            publicUserName = userName;
            publicUserRole = userRole;

            if (userRole == 1)
            {
                MessageBox.Show("Вы вошли как админ!");
            }
            else
            {
                MessageBox.Show("Ты бедный пользователь, ухади!(");

                menuClients.Foreground = Brushes.Gray;
            }
        }

        private void goBackBtn_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new LogInWindow();

            newWindow.Show();
            this.Close();
        }
    }
}
