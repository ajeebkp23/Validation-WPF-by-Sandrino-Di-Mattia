using System;

namespace POC.Validatie.Model.Validation
{
    interface IValidationRule
    {
        void Validate(object value, out bool isValid, out string errorMessage);
    }
}
