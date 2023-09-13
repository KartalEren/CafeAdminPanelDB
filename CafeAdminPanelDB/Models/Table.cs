namespace CafeAdminPanelDB.Models
{
    public class Table : Base
    {
        public int TableId { get; set; }
        public int TableNo { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Table()
        {
                Orders = new List<Order>();
        }
    }
}
