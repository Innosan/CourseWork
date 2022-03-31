using System;
using System.Linq;
using FluentValidation;

namespace CourseWork.Validators
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Поле пустое!")
                .Length(3, 25).WithMessage("Введите корректное имя!")
                .Must(IsValidName).WithMessage("В имени есть неприемлимые символы!");

            RuleFor(user => user.UserLogin)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Поле пустое!")
                .Length(3, 25).WithMessage("Введите корректный логин!")
                .Must(IsValidLogin).WithMessage("В логине есть неприемлимые символы!");

            RuleFor(user => user.UserMail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Поле пустое!")
                .EmailAddress().WithMessage("Это не почта!")   // чек на собаку
                .Must(IsValidMail).WithMessage("Почта содержит неприемлимые символы!");

            RuleFor(user => user.UserPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Поле пустое!")
                .Length(3, 15).WithMessage("Пароль слишком короткий!")
                .Must(IsValidPassword).WithMessage("В пароле есть неприемлимые символы!");
            
        }

        protected bool IsValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(Char.IsLetter);
        }

        protected bool IsValidLogin(string login)
        {
            login = login.Replace("_", "");

            return login.All(Char.IsLetterOrDigit);
        }

        protected bool IsValidMail(string mail)
        {
            mail = mail.Replace("_", "");
            mail = mail.Replace("-", "");
            mail = mail.Replace("@", "");
            mail = mail.Replace(".", "");

            return mail.All(Char.IsLetterOrDigit);
        }

        protected bool IsValidPassword(string password)
        {
            password = password.Replace("_", "");
            password = password.Replace(".", "");
            password = password.Replace("-", "");

            return password.All(Char.IsLetterOrDigit);
        }
    }
}
