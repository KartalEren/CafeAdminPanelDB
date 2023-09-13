using System.ComponentModel.DataAnnotations.Schema;

namespace CafeAdminPanelDB.Models
{
    public class Product : Base
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public IFormFile Dosya { get; set; }//resmin dosyasını tutar. IFormFile: dosyaların içeriğini tutar.
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
                OrderDetails = new List<OrderDetail>();
        }
    }
}