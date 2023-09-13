using CafeAdminPanelDB.Data;
using CafeAdminPanelDB.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CafeAdminPanelDB.Controllers
{
    public class UserController : Controller
    {//HERKESİN GÖRECEĞİ YER İÇİN USERCONTROLLER YAPTIK

        private readonly AdminPanelDbContext _db;
        public UserController(AdminPanelDbContext db) //db yi burada newleriz
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ColdDrinks()
        {
            List<ProductListVM> products = _db.Products.Where(x => x.Type == Models.Type.ColdDrink).Select(x => new ProductListVM
            {
                Id = x.ID,
                Name = x.Name,
                Price= x.Price,
                ImagePath = x.ImagePath
            }).ToList();


            return PartialView("_ProductListPV", products);// return PartialView yaparsak bu şekil, View oluşturmaya gerek yok direkt View var gibi gönderir.
        } public IActionResult HotDrinks()
        {
            List<ProductListVM> products = _db.Products.Where(x => x.Type == Models.Type.HotDrink).Select(x => new ProductListVM
            {
                Id = x.ID,
                Name = x.Name,
                Price= x.Price,
                ImagePath = x.ImagePath
            }).ToList();


            return PartialView("_ProductListPV", products);// return PartialView yaparsak bu şekil, View oluşturmaya gerek yok direkt View var gibi gönderir.
        } public IActionResult Dessert()
        {
            List<ProductListVM> products = _db.Products.Where(x => x.Type == Models.Type.Dessert).Select(x => new ProductListVM
            {
                Id = x.ID,
                Name = x.Name,
                Price= x.Price,
                ImagePath = x.ImagePath
            }).ToList();


            return PartialView("_ProductListPV", products);// return PartialView yaparsak bu şekil, View oluşturmaya gerek yok direkt View var gibi gönderir.
        } public IActionResult Food()
        {
            List<ProductListVM> products = _db.Products.Where(x => x.Type == Models.Type.Food).Select(x => new ProductListVM
            {
                Id = x.ID,
                Name = x.Name,
                Price= x.Price,
                ImagePath = x.ImagePath
            }).ToList();


            return PartialView("_ProductListPV", products);// return PartialView yaparsak bu şekil, View oluşturmaya gerek yok direkt View var gibi gönderir.
        }

    }
}
