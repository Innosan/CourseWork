using System;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using CourseWork.Validators;
using FluentValidation.Results;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        ApplicationContext db;

        BindingList<string> errors = new BindingList<string>();

        public RegistrationWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();

            passBox.GotFocus += new RoutedEventHandler(passBox_Focus);

            passBox.Password = "samplepass";

            Binding binding = new Binding();
            binding.Source = errors;
        }

        private void passBox_Focus(object sender, RoutedEventArgs e)
        {
            passBox.Password = "";

            passBox.Foreground = Brushes.White;
        }

        private void toSignInHypLink_Click(object sender, RoutedEventArgs e)
        {
            var newForm = new LogInWindow();

            newForm.Show();
            this.Close();
        }

        private void logInBtn_Click(object sender, RoutedEventArgs e)
        {
            errors.Clear();

            string name = nameBox.Text;
            string login = loginBox.Text;
            string mail = mailBox.Text;
            string password = passBox.Password;

            List<TextBlock> errorBlocks = new List<TextBlock> { UserName, UserLogin, UserMail, UserPassword };

            User user = new User(login, password, name, mail);

            UserValidator validator = new UserValidator();

            FluentValidation.Results.ValidationResult results = validator.Validate(user);

            if (results.IsValid == false)
            {
                foreach (ValidationFailure error in results.Errors)
                {
                    foreach (TextBlock item in errorBlocks)
                    {
                        if (error.PropertyName == item.Name)
                        {
                            item.Text = error.ErrorMessage;
                        }
                    }
                }
            }
            else
            {
                /*MessageBox.Show("Поля валидны!");*/
                db.Users.Add(user);
                db.SaveChanges();

                foreach (TextBlock textBlock in errorBlocks)
                {
                    textBlock.Text = "";
                }
            }

        }
    }
}
