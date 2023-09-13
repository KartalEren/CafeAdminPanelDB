using CafeAdminPanelDB.Models;

namespace CafeAdminPanelDB.ViewModel
{
    public class OrderDetailVM:Base
    {
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? OrderDate { get; set; }
        //public Order Order { get; set; } 
        public int OrderId { get; set; }
        //public Product Product { get; set; }birbirleriyle bağlantılı olduğu için fk lardan dolayı ürün adı çekerken employeecontrollerda details actionda çağırma (bağlama işlemi yaptık)
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public string ProductImage { get; set; }
        public int TableNo { get; set; }

    }
}
