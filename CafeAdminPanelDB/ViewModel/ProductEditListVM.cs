using CafeAdminPanelDB.CustomValidation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CafeAdminPanelDB.ViewModel
{
    public class ProductEditListVM
    {
        public int Id { get; set; }


        [ProductName()]
        public string Name { get; set; }


        [DataType(DataType.Currency)]
        [ProductPricePositive()]
        [ProductPriceAttiribute(20, 150)]
        public decimal Price { get; set; }
        public Models.Type Type { get; set; }
        public string Description { get; set; }


        [NotMapped]
        [ProductImagePathEditAttiribute("png", "jpg")]
        public IFormFile Dosya { get; set; }
    }
}
