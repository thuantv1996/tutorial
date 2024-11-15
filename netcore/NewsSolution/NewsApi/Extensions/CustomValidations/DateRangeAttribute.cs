using NewsApi.Extensions.CustomTypes;
using System.ComponentModel.DataAnnotations;

namespace NewsApi.Extensions.CustomValidations
{
    public class DateRangeAttribute : ValidationAttribute
    {
        public string InvalidTypeError { get; set; } = "Value is not a date range";
        public DateRangeAttribute()
        {
            ErrorMessage = "Start date is later than end date";
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // check null value
            if (value == null) return ValidationResult.Success;

            // check type is date range
            if (value.GetType() != typeof(DateRange)) return new ValidationResult(InvalidTypeError);

            var dateRange = (DateRange)value;

            if(dateRange.StartDate.CompareTo(dateRange.EndDate) > 0)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
