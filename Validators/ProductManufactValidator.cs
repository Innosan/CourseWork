using FluentValidation;
using System;
using System.Linq;

namespace CourseWork.Validators
{
    internal class ProductManufactValidator : AbstractValidator<Product>
    {
        public ProductManufactValidator()
        {
            string fieldName = "Название компании";

            RuleFor(product => product.ProdManufacturer)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{fieldName} пустое!")
                .Length(3, 25).WithMessage($"{fieldName} слишком короткое или длинное!")
                .Must(BeValidName).WithMessage($"{fieldName} содержит неприемлимые символы!")
                .Must(StartWithCapital).WithMessage($"{fieldName} должно начинаться\nс большой буквы!");
        }

        protected bool BeValidName(string manufact)
        {
            manufact = manufact.Replace(" ", "");
            manufact = manufact.Replace("-", "");

            return manufact.All(Char.IsLetterOrDigit);
        }

        protected bool StartWithCapital(string manufact)
        {
            return Char.IsUpper(manufact[0]);
        }
    }
}
