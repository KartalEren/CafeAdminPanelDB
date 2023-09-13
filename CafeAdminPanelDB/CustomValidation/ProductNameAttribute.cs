using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CafeAdminPanelDB.CustomValidation
{
    public class ProductNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Boş geçilemez.");
            }



            string productName = (string)value;
            string istenen = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(productName.ToLower());



            if (productName != istenen)
            {
                return new ValidationResult("Ürün adının her kelimesi büyük harfle başlamalı.");
            }



            return ValidationResult.Success;
        }
    }
}
