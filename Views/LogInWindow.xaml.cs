using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CourseWork
{
    public partial class LogInWindow : Window
    {
        int wrongLoginErrorCnt = 0;

        public LogInWindow()
        {
            InitializeComponent();

            passBox.GotFocus += new RoutedEventHandler(passBox_Focus);

            passBox.Foreground = Brushes.Gray;

            wrongLoginErrorCnt = 0;
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

            if (authUser == null)
            {
                wrongLoginErrorCnt++;

                MessageBox.Show("Такой пользователь не найден!");

                if (wrongLoginErrorCnt % 3 == 0)
                {
                    GenerateCaptchaText();

                    captchaPopup.IsEnabled = true;

                    DisableControlsOnCaptcha();
                }
            }
            else
            {
                var newForm = new MainWindow(authUser.UserName, authUser.UserRole);

                newForm.Show();
                this.Close();
            }
                
        }

        private void DisableControlsOnCaptcha()
        {
            loginBox.IsEnabled = !loginBox.IsEnabled;
            passBox.IsEnabled = !passBox.IsEnabled;
            toRegistrationHypLink.IsEnabled = !toRegistrationHypLink.IsEnabled;
        }

        private void GenerateCaptchaText()
        {
            string path = Path.GetRandomFileName();

            path = path.Replace(".", "");
            captchaTextBlock.Text =  path.Substring(3, 6);

            captchaPopup.IsOpen = true;
        }

        private void captchaAcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            if (userCaptchaTextBlock.Text == captchaTextBlock.Text)
            {
                captchaPopup.IsOpen = false;

                DisableControlsOnCaptcha(); //enable
            }
            else
            {
                captchaPopup.IsOpen = false;
                MessageBox.Show("Проверка не пройти, -100 социальный кредит и кошка жена убить, миска рис украсть!1!\n\nヽ(≧Д≦)ノ ");
                captchaPopup.IsOpen = true;

                GenerateCaptchaText();
            }
        }
    }
}
