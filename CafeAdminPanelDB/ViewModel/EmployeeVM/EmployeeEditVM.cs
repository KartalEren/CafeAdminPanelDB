using CafeAdminPanelDB.Models;

namespace CafeAdminPanelDB.ViewModel.EmployeeVM
{
    public class EmployeeEditVM
    {
        public int TableId { get; set; }
        public ICollection<TableListVM> TableListVM { get; set; }
        public ICollection<ProductListVM> ProductListVM { get; set; }
        public int? Quantity { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? UnitPrice { get; set; }
        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public EmployeeEditVM()
        {
            TableListVM = new HashSet<TableListVM>();
            ProductListVM = new HashSet<ProductListVM>();
        }
    }
}
