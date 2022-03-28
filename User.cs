using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    internal class User
    {
        public int userId { get; set; }

        private string userLogin;
        private string userPassword;
        private string userName;
        private string userMail;

        public string UserLogin { get { return userLogin; } set { userLogin = value; } }
        public string UserPassword { get { return userPassword; } set { userPassword = value; } }
        public string UserName { get { return userName; } set { userName = value; } }
        public string UserMail { get { return userMail; } set { userMail = value; } }

        public User() { }

        public User(string login, string password, string name, string mail)
        {
            this.userLogin = login;
            this.userPassword = password;
            this.userName = name;
            this.userMail = mail;
        }
    }
}
