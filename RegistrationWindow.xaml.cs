using System.Windows;
using System.Windows.Media;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();

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
    }
}
