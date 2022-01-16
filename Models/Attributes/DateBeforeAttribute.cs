using System;
using System.ComponentModel.DataAnnotations;

namespace TestAppProject.Models.Attributes
{
    // Атрибут для валидации того, что дата регистрации меньше чем дата последней активности
    public class DateBeforeAttribute : ValidationAttribute
    {
        private readonly string comparisonPropertyName;

        public DateBeforeAttribute(string propertyName)
        {
            comparisonPropertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(comparisonPropertyName);

            if (property == null)
                throw new ArgumentException("Указанное свойство не найдено");

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (currentValue > comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
