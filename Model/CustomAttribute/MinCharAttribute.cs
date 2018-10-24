using System.ComponentModel.DataAnnotations;

namespace FormValidation.CustomAttribute
{
    public class MinCharAttribute : ValidationAttribute
    {
        private int _minAge;
        public MinCharAttribute(int value)
        {
            _minAge = value;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is int)
                {
                    int minimumage = (int)value;
                    if (minimumage < _minAge)
                    {
                        return new ValidationResult("Số ký tự tối thiểu là:"  + _minAge);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}