using System;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        ApplicationContext db;

        public RegistrationWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();

            passBox.GotFocus += new RoutedEventHandler(passBox_Focus);

            passBox.Password = "samplepass";
        }

        private void passBox_Focus(object sender, RoutedEventArgs e)
        {
            passBox.Password = "";

            passBox.Foreground = Brushes.White;
        }

        private void toSignInHypLink_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new MainWindow();

            newForm.Show();
            this.Close();
        }

        private void logInBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = nameBox.Text;
            string login = loginBox.Text;
            string mail = mailBox.Text;
            string password = passBox.Password;

            User user = new User(login, password, name, mail);

            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
