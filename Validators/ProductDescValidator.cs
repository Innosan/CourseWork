using FluentValidation;
using System;

namespace CourseWork.Validators
{
    internal class ProductDescValidator : AbstractValidator<Product>
    {
        public ProductDescValidator()
        {
            string fieldName = "Описание";

            RuleFor(product => product.ProdDescription)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{fieldName} пустое!")
                .Length(3, 120).WithMessage($"{fieldName} слишком короткое или длинное!")
                .Must(StartWithCapital).WithMessage($"{fieldName} должно начинаться с большой буквы!");
        }

        protected bool StartWithCapital(string desc)
        {
            return Char.IsUpper(desc[0]);
        }
    }
}
