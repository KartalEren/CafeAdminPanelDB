using CafeAdminPanelDB.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace CafeAdminPanelDB.CustomValidation
{
    public class ProductImagePathEditAttiribute : ValidationAttribute
    {

        public string[] ImageTypes { get; set; }



        public ProductImagePathEditAttiribute(params string[] imageTypes)
        {
            ImageTypes = imageTypes;
        }



        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var Product = (ProductEditListVM)validationContext.ObjectInstance;
            var image = value;



            if (!(ImageTypes.Any(x => Product.Dosya.FileName.EndsWith(x))))
            {
                string msg = "Sadece ";
                for (int i = 0; i < ImageTypes.Length; i++)
                {
                    msg += ImageTypes[i] + ", ";
                }
                msg += "türünde görsel yükleyin.";      //string join, string builder += yerine araştır!!!



                return new ValidationResult(msg);
            }



            return ValidationResult.Success;
        }
    }
}



