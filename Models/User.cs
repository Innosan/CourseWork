using System.ComponentModel.DataAnnotations;

namespace CourseWork
{
    internal class User
    {
        [Key]
        public int userId { get; set; }

        private string userLogin;
        private string userPassword;
        private string userName;
        private string userMail;
        private int userRole;

        public string UserLogin { get { return userLogin; } set { userLogin = value; } }
        public string UserPassword { get { return userPassword; } set { userPassword = value; } }
        public string UserName { get { return userName; } set { userName = value; } }
        public string UserMail { get { return userMail; } set { userMail = value; } }

        public int UserRole { get { return userRole; } set { userRole = value; } }

        public User() { }

        public User(string login, string password, string name, string mail, int role)
        {
            this.userLogin = login;
            this.userPassword = password;
            this.userName = name;
            this.userMail = mail;
            this.userRole = role;
        }
    }
}
