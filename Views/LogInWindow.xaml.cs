﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace CourseWork
{
    public partial class LogInWindow : Window
    {
        public LogInWindow()
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

        private void toSignUpHypLink_Click(object sender, EventArgs e)
        {
            var newForm = new RegistrationWindow();

            newForm.Show();
            this.Close();
        }

        private void logInBtn_Click(object sender, RoutedEventArgs e)
        {
            User authUser = null;

            using (ApplicationContext context = new ApplicationContext())
            {
                authUser = context.Users.Where(b => b.UserLogin == loginBox.Text && b.UserPassword == passBox.Password).FirstOrDefault();
            }

            if (authUser != null)
            {
                var newForm = new MainWindow(authUser.UserName, authUser.UserRole);

                newForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует!");
            }
        }
    }
}