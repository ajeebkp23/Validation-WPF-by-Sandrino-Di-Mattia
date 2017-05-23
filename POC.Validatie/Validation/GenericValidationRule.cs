using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using POC.Validatie.Model.Validation;

namespace POC.Validatie
{
    class GenericValidationRule : ValidationRule
    {
        private IValidationRule ValidationRule;

        public GenericValidationRule(IValidationRule validationRule)
        {
            this.ValidationRule = validationRule;
            this.ValidatesOnTargetUpdated = true;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            bool isValid = false;
            string errorMessage = "";

            ValidationRule.Validate(value, out isValid, out errorMessage);

            ValidationResult result = new ValidationResult(isValid, errorMessage);
            return result;
        }
    }
}
