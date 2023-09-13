using CafeAdminPanelDB.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeAdminPanelDB.ViewModel
{
    public class ProductCreateListVM
    {
        [ProductNameAttribute()]
        public string Name { get; set; }

        [ProductPricePositiveAttribute()]
        [DataType(DataType.Currency)]
        [ProductPriceAttiribute(20, 150)]
        public decimal Price { get; set; }
        public Models.Type Type { get; set; }
        public string Description { get; set; }


        [ProductImagePathAttiribute("png", "jpg")]
        [NotMapped]
        public IFormFile Dosya { get; set; }
    }
}
