using System.ComponentModel.DataAnnotations;

namespace CafeAdminPanelDB.CustomValidation
{
    public class ProductPriceAttiribute: ValidationAttribute
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }

        public ProductPriceAttiribute(double minPrice, double maxPrice)
        {
            MinPrice=minPrice; 
            MaxPrice=maxPrice;
        }


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Boş geçilemez.");
            }


            decimal price = (decimal)value;

            if(price<(decimal)MinPrice || price > (decimal)MaxPrice)
            {
                return new ValidationResult($"Fiyat aralığı {MinPrice} ile {MaxPrice} arasında olmalıdır.");
            }

            return ValidationResult.Success;



        }





    }
}
