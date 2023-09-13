using System.ComponentModel.DataAnnotations;

namespace CafeAdminPanelDB.CustomValidation
{
    public class ProductPricePositiveAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Boş geçilemez.");
            }



            decimal price = (decimal)value;



            if (price <= 0)
            {
                return new ValidationResult("Fiyat aralığı 0'dan büyük olmalıdır.");
            }



            return ValidationResult.Success;
        }
    }
}
