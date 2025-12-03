using System.Collections.Generic;
using System.Linq;

namespace GymWpfApp.Validators
{
    /// <summary>
    /// Represents the result of a validation operation
    /// </summary>
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }

        public ValidationResult()
        {
            IsValid = true;
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            IsValid = false;
            Errors.Add(error);
        }

        public string GetErrorMessage()
        {
            return string.Join("\n", Errors);
        }

        public bool HasErrors => Errors.Any();
    }
}
