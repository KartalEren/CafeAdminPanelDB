using CafeAdminPanelDB.Models;

namespace CafeAdminPanelDB.ViewModel.UserVM
{
    public class UserVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
