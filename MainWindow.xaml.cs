using System.Windows;

namespace CourseWork
{
    public partial class MainWindow : Window
    {
        public string[] publicUserName;
        public int publicUserRole;

        public MainWindow(string userName, int userRole)
        {
            InitializeComponent();

            publicUserName = userName.Split(' ');
            publicUserRole = userRole;

            if (userRole == 1)
            {
                MessageBox.Show("Вы вошли как админ!");
            }
            else
            {
                MessageBox.Show("Ты бедный пользователь, ухади!(");

                menuClients.TextDecorations = TextDecorations.Strikethrough;
            }

            greetingLabel.Text = "Добро пожаловать,\n" + publicUserName[0] + "!";
        }

        private void goBackBtn_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new LogInWindow();

            newWindow.Show();
            this.Close();
        }
    }
}
