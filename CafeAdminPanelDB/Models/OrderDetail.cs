namespace CafeAdminPanelDB.Models
{
    public class OrderDetail : Base
    {
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? OrderDate { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }

    }
}
