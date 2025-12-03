namespace GymWpfApp.Validators
{
    /// <summary>
    /// Validator for Equipment entities
    /// </summary>
    public static class EquipmentValidator
    {
        /// <summary>
        /// Validates equipment fields
        /// </summary>
        public static ValidationResult ValidateEquipment(string code, string name, string category)
        {
            var result = new ValidationResult();

            // Validate Code
            if (string.IsNullOrWhiteSpace(code))
            {
                result.AddError("Mã thiết bị không được để trống!");
            }

            // Validate Name
            if (string.IsNullOrWhiteSpace(name))
            {
                result.AddError("Tên thiết bị không được để trống!");
            }

            // Validate Category
            if (string.IsNullOrWhiteSpace(category))
            {
                result.AddError("Loại thiết bị không được để trống!");
            }

            return result;
        }
    }
}
