using FluentValidation;
using System;
using System.Linq;

namespace CourseWork.Validators
{
    internal class ProductNameValidator : AbstractValidator<Product>
    {
        public ProductNameValidator()
        {
            string fieldName = "Название";

            RuleFor(product => product.ProdName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{fieldName} пустое!")
                .Length(3, 25).WithMessage($"Введите корректное {fieldName}!")
                .Must(BeValidName).WithMessage($"{fieldName} содержит неприемлимые символы!")
                .Must(StartWithCapital).WithMessage($"{fieldName} должно начинаться\n с большой буквы!");
        }

        protected bool BeValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(Char.IsLetter);
        }

        protected bool StartWithCapital(string name)
        {
            return Char.IsUpper(name[0]);
        }
    }
}
