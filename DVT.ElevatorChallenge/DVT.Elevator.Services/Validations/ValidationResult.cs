using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Services.Validations
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string ErrorMessage { get; }
        private ValidationResult(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }
        public static ValidationResult Success() => new ValidationResult(true, string.Empty);
        public static ValidationResult Failure(string errorMessage) => new ValidationResult(false, errorMessage);
    }
}
