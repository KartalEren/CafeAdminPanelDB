using CafeAdminPanelDB.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeAdminPanelDB.ViewModel
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string ImagePath { get; set; }       
        public decimal Price { get; set; }        
        [NotMapped]
        public IFormFile Dosya { get; set; }
        public Models.Type Type { get; set; }
        public string Description { get; set; }
    }
}
