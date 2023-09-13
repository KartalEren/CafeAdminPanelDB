namespace CafeAdminPanelDB.ViewModel
{
    public class TakeOrderVM
    {
        public int TableNo { get; set; }
        public List<TableListVM> TableListVMs { get; set; }
        public List<ProductListVM> ProductListVMs { get; set; }
        public TakeOrderVM()
        {
            TableListVMs = new List<TableListVM>();
            ProductListVMs = new List<ProductListVM>();
        }

    }
}
