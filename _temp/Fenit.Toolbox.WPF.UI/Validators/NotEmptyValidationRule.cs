using System.Globalization;
using System.Windows.Controls;

namespace Fenit.Toolbox.WPF.UI.Validators
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, null);

            if (value is string)
            {
                var sval = value as string;
                return new ValidationResult(!string.IsNullOrEmpty(sval), null);
            }

            return new ValidationResult(true, null);
        }
    }
}