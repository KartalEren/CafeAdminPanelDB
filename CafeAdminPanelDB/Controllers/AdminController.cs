using CafeAdminPanelDB.CustomActionFilter;
using CafeAdminPanelDB.Data;
using CafeAdminPanelDB.Models;
using CafeAdminPanelDB.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CafeAdminPanelDB.Controllers
{
    //[LoginActionFilter]//GİRİŞ BAŞARILIYSA BURAYA GÖNDERİR. FAKAT ADMİNE GİRİŞTE LOGİN YAPARAK GİRMESİNİ İSTEDİĞİMİZ İÇİN VE LOGİN YAPACAK USER YARATMADIĞIMIZ İÇİN [LoginActionFilter] HATA VERMEMESİ İÇİN PASİF DURUMA ALDIK
    public class AdminController : Controller
    {
        private readonly AdminPanelDbContext _db;//db yi burada ctor unda aşağıda newlemek gerekir public için



        private readonly IWebHostEnvironment _whe;//root klasörüne resim atmak için gerekli yapılardır
        //public AdminController(IWebHostEnvironment whe)//root klasörüne resim atmak için gerekli yapılardır
        //{
        //    _whe = whe;
        //}

        public AdminController(AdminPanelDbContext db, IWebHostEnvironment whe)
        {
            _db = db;
            _whe = whe;
        }



        // GET: AdminController
        public IActionResult Index()
        {
            List<ProductListVM> products = _db.Products.Select(x => new ProductListVM()
            {
                Id = x.ID,
                Name = x.Name,
                ImagePath = x.ImagePath,
                Price = x.Price,
            }).ToList();
            return View(products);
        }









        // GET: AdminController/Details/5
        public IActionResult Details(int id)
        {
            //return View(_db.Products.FirstOrDefault(x => x.ID == id));

            Product product = _db.Products.FirstOrDefault(x => x.ID == id);

            ProductListVM productListVM = new ProductListVM()
            {
                Id=product.ID,
                ImagePath=product.ImagePath,
                Name = product.Name,
                Description = product.Description,
                Dosya = product.Dosya,
                Price = product.Price,
                Type = product.Type
            };
            return View(productListVM);
        }







        // GET: AdminController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreateListVM productVM)
        {
            if (!ModelState.IsValid)
            {
                return View(productVM);
            }
            try
            {
                string img = string.Empty;



                if (productVM == null)
                {
                    throw new Exception("Boş nesne");
                }
                if (productVM.Dosya == null)
                {
                    throw new Exception("Resim eklemeden ürün oluşturulamaz!");
                }



                var dosyaYolu = Path.Combine(_whe.WebRootPath, "NewFolder1");
                if (!Directory.Exists(dosyaYolu))
                {
                    Directory.CreateDirectory(dosyaYolu);
                }



                if (productVM.Dosya != null)
                {
                    var tamDosyaAdı = Path.Combine(dosyaYolu, productVM.Dosya.FileName);
                    using (var dosyaAkisi = new FileStream(tamDosyaAdı, FileMode.OpenOrCreate))
                    {
                        productVM.Dosya.CopyToAsync(dosyaAkisi);
                    }



                    img = Path.Combine(@"\NewFolder1\", productVM.Dosya.FileName);
                }



                Product product = new Product()// burada kullanıcıdan alınan değerleri tabloya atarız.*****
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    ImagePath = img,
                    Price = productVM.Price,
                    Dosya = productVM.Dosya,
                    Type = productVM.Type
                };



                _db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(productVM);
            }
        }




        // GET: AdminController/Edit/5
        public IActionResult Edit(int id)
        {
            Product product = _db.Products.FirstOrDefault(x => x.ID == id);

            ProductEditListVM productEditListVM = new ProductEditListVM()// burada tablodaki değerleri vm tabloya atarız.*****
            {
                Name = product.Name,
                Description = product.Description,
                Dosya = product.Dosya,
                Price = product.Price,
                Type = product.Type
            };
            return View(productEditListVM);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductEditListVM productEditListVM, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(productEditListVM);
            }
            try
            {
                Product product = _db.Products.FirstOrDefault(x => x.ID == productEditListVM.Id);// burada kullanıcıdan alınan değerleri tabloya atarız.*****
                product.Name = productEditListVM.Name;
                product.Price = productEditListVM.Price;
                product.Dosya = productEditListVM.Dosya;
                product.Description = productEditListVM.Description;
                product.Type = productEditListVM.Type;


                ResimYukle(product, id);
                _db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;//yeni y  apılanı düzenleyerek ekle demek.
                //_db.Products.Add(product); üstteki eşlemeyi yaptığımız için buna gerek yok
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(productEditListVM);
            }
        }




        // GET: AdminController/Delete/5
        public IActionResult Delete(int id)
        {
            Product product = _db.Products.FirstOrDefault(x => x.ID == id);

            ProductListVM productListVM = new ProductListVM()// burada tablodaki değerleri vm tabloya atarız.*****
            {
                Id = product.ID,
                Name = product.Name,
                Price = product.Price,
                ImagePath = product.ImagePath,
                Description = product.Description,
                Dosya = product.Dosya,
                Type = product.Type
            };
            return View(productListVM);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ProductListVM productListVM)
        {
            try
            {
                Product silinecek = _db.Products.FirstOrDefault(x => x.ID == productListVM.Id);
                _db.Products.Remove(silinecek);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(productListVM);
            }
        }





        private void ResimYukle(Product product, int id)
        {
            var dosyaYolu = Path.Combine(_whe.WebRootPath, "NewFolder1");
            if (!Directory.Exists(dosyaYolu))
            {
                Directory.CreateDirectory(dosyaYolu);
            }



            if (product.Dosya == null)
            {
                product.ImagePath = _db.Products.FirstOrDefault(x => x.ID == id).ImagePath;
            }
            else
            {
                var tamDosyaAdı = Path.Combine(dosyaYolu, product.Dosya.FileName);
                using (var dosyaAkisi = new FileStream(tamDosyaAdı, FileMode.OpenOrCreate))
                {
                    product.Dosya.CopyToAsync(dosyaAkisi);
                }



                product.ImagePath = Path.Combine(@"\NewFolder1\", product.Dosya.FileName);
            }
        }






        //private async Task ImageCreate(Product product, IFormFile imgFile)  //Resim yükleme metodu, wwwroot un içine KENDİN KLASÖR OLUŞTUR BURAYA DA DOSYA ADINI YAZ.
        //{


        //    var saveimg = Path.Combine(_whe.WebRootPath, "NewFolder", imgFile.FileName);



        //    string imgext = Path.GetExtension(imgFile.FileName);
        //    if (imgext == ".jpg" || imgext == ".png")
        //    {
        //        using (var uploading = new FileStream(saveimg, FileMode.Create))
        //        {
        //            await imgFile.CopyToAsync(uploading);
        //            ViewData["Message"] = "The Selected File" + imgFile.FileName + "Is Saved Successfully";
        //        }
        //    }
        //    else
        //    {
        //        ViewData["Message"] = "Resim tipi yalnızda jpg ve png olmalıdır";
        //    }
        //    product.ImagePath = @$"/NewFolder/{imgFile.FileName}";
        //}
    }
}
