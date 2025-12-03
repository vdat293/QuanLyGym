using GymWpfApp.Constants;
using System.Text.RegularExpressions;

namespace GymWpfApp.Validators
{
    /// <summary>
    /// Validator for Person-related entities (Member, Staff)
    /// </summary>
    public static class PersonValidator
    {
        /// <summary>
        /// Validates common person fields (name, phone, age)
        /// </summary>
        public static ValidationResult ValidatePersonFields(string name, string phone, int age)
        {
            var result = new ValidationResult();

            // Validate Name
            if (string.IsNullOrWhiteSpace(name))
            {
                result.AddError("Tên không được để trống!");
            }

            // Validate Phone
            if (string.IsNullOrWhiteSpace(phone))
            {
                result.AddError("Số điện thoại không được để trống!");
            }
            else if (!IsValidPhone(phone))
            {
                result.AddError(AppConstants.Messages.ErrorInvalidPhone);
            }

            // Validate Age
            if (age < AppConstants.Validation.MinAge || age > AppConstants.Validation.MaxAge)
            {
                result.AddError(AppConstants.Messages.ErrorInvalidAge);
            }

            return result;
        }

        /// <summary>
        /// Checks if phone number is valid (10-11 digits)
        /// </summary>
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Remove spaces and special characters
            string cleanPhone = Regex.Replace(phone, @"[^\d]", "");

            return cleanPhone.Length >= AppConstants.Validation.MinPhoneLength &&
                   cleanPhone.Length <= AppConstants.Validation.MaxPhoneLength;
        }
    }
}
