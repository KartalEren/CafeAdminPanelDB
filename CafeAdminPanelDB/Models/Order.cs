namespace CafeAdminPanelDB.Models
{
    public class Order : Base
    {
        public int TableID { get; set; }
        public Table Table { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
                OrderDetails = new List<OrderDetail>();
        }

    }
}
