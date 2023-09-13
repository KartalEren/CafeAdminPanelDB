namespace CafeAdminPanelDB.ViewModel.EmployeeVM
{
    public class EmployeeDeleteVM
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? OrderDate { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
