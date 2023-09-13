using CafeAdminPanelDB.Data;
using CafeAdminPanelDB.Models;
using CafeAdminPanelDB.ViewModel;
using CafeAdminPanelDB.ViewModel.EmployeeVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CafeAdminPanelDB.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly AdminPanelDbContext _db;

        public EmployeeController(AdminPanelDbContext db) //db yi burada newleriz
        {
            _db = db;
        }



        // GET: EmployeeController
        public ActionResult Index()
        {
            List<OrderDetailVM> ordersDetailVMs = _db.OrderDetails.Where(x => x.OrderDate >= DateTime.Today).Select(x => new OrderDetailVM()// burada tablodaki değerleri vm tabloya atarız.*****
            {
                ID = x.ID,
                OrderDate = x.OrderDate,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                ProductName = x.Product.Name,
                TableNo = x.Order.Table.TableNo,
                OrderId = x.OrderId,
            }).OrderBy(x => x.ID).ToList();

            return View(ordersDetailVMs);
        }







        [HttpGet]
        public ActionResult TakeOrder()
        {
            TakeOrderVM takeOrderVM = new TakeOrderVM();
            takeOrderVM.ProductListVMs = _db.Products.Select(x => new ProductListVM// burada tablodaki değerleri vm tabloya atarız.*****
            {
                Id = x.ID,
                Name = x.Name,
                ImagePath = x.ImagePath,
                Price = x.Price,
            }).ToList();

            takeOrderVM.TableListVMs = _db.Tables.Select(x => new TableListVM// burada tablodaki değerleri vm tabloya atarız.*****
            {
                TableId = x.ID,
                TableNo = x.TableNo
            }).ToList();
            return View(takeOrderVM);

        }



        [HttpPost]
        public ActionResult TakeOrder(IFormCollection keyValuePairs, TakeOrderVM takeOrderVM)
        {
            string[] count = keyValuePairs["count"].Where(x => x != "0" && x != "").ToArray();

            string[] productNames = keyValuePairs["product"];
            int tableNo;
            bool isTableNumber = int.TryParse(keyValuePairs["tableNo"], out tableNo);

            if (count.Length == 0 || productNames.Length == 0 || !isTableNumber)
            {
                takeOrderVM.ProductListVMs = _db.Products.Select(x => new ProductListVM
                {
                    Id = x.ID,
                    Name = x.Name,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                }).ToList();


                takeOrderVM.TableListVMs = _db.Tables.Select(x => new TableListVM
                {
                    TableId = x.ID,
                    TableNo = x.TableNo
                }).ToList();

                return View(takeOrderVM);

            }

            Order order = new Order()
            {
                TableID = tableNo,
                Table = _db.Tables.FirstOrDefault(x => x.TableNo == tableNo)
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            for (int i = 0; i < productNames.Length; i++)
            {
                Product product = _db.Products.FirstOrDefault(x => x.Name == productNames[i]);

                OrderDetail orderDetail = new OrderDetail()
                {
                    Order = order,
                    OrderId = order.ID,
                    OrderDate = DateTime.Now,
                    Product = product,
                    ProductId = product.ID,
                    Quantity = int.Parse(count[i]),
                    UnitPrice = product.Price,
                };
                orderDetails.Add(orderDetail);
                product.OrderDetails.Add(orderDetail);

            }
            order.OrderDetails = orderDetails;

            _db.OrderDetails.AddRange(orderDetails);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }




        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            OrderDetailVM orderDetailVM = new OrderDetailVM();//OrderDetailVM de olmayan TableNo,ProductImage ve ProductName ürünlerini buradaki değişkene aşağıda atamak için new ledik ki detayları getirebilsin 

            Product product = _db.Products.First(x => x.OrderDetails.Any(o => o.ID == id));//BURADA ÜRÜN İSİMLERİNE ORDERDETAİLS DAN ULAŞMAK İÇİN ÜRÜNLER LİSTESİNDEN GİTMEK GEREKLİDİR, ORDERDETAİLS HİÇ İÇERİYOR MU PRODUCTS DA KARŞILIK GELEN ÜRÜN ID SİNE... DİYEREK ÇEKMİŞ OLURUZ.
            Order order = _db.Orders.FirstOrDefault(x => x.OrderDetails.Any(x => x.ID == id));//AŞAĞIDAKİ İLE JOIN LEME GİBİ OLDU ARA BAĞLANTI.  (orderdetailden order a atama yapıyoruz, aşağıda da order dan table a geçiyoruz)
            Models.Table table = _db.Tables.FirstOrDefault(x => x.Orders.Any(o => o.ID == order.ID));//YUKARIDAKİ ORDER DAN TABLE GEÇİŞ YAPTIK

            orderDetailVM.ProductName = product.Name;
            orderDetailVM.ProductImage = product.ImagePath;
            orderDetailVM.TableNo = table.TableNo;
            orderDetailVM.OrderDetail = _db.OrderDetails.First(X => X.ID == id);

            //OrderDetail orderDetail = _db.OrderDetails.First(x => x.ID == id);
            return View(orderDetailVM);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }





        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            OrderDetail orderDetail = _db.OrderDetails.First(x => x.ID == id);
            Order order = _db.Orders.First(x => x.OrderDetails.Any(o => o.ID == id));

                          
            EmployeeEditVM employeeEditVM = new EmployeeEditVM()// burada tablodaki değerleri vm tabloya atarız.*****
            {
                OrderDetailId = orderDetail.ID,

                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice,
                OrderDate = orderDetail.OrderDate,
                OrderId = orderDetail.OrderId,
                TableId = order.TableID,


                ProductListVM = _db.Products.Select(x => new ProductListVM
                {
                    Id = x.ID,
                    Name = x.Name,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                }).ToList(),

                TableListVM = _db.Tables.Select(x => new TableListVM
                {
                    TableId = x.TableId,
                    TableNo = x.TableNo,
                }).ToList()
            };

            return View(employeeEditVM);
        }



        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection keyValuePairs, EmployeeEditVM employeeEditVM)
        {
            try
            {
                int productName = Convert.ToInt32(keyValuePairs["product"]);//combobox için bu keyleri oluşturuyoruz.

                int tableNo;
                bool isTableNo = int.TryParse(keyValuePairs["tableNo"], out tableNo); //combobox için bu keyleri oluşturuyoruz.

                Order order = _db.Orders.FirstOrDefault(x => x.ID == employeeEditVM.OrderId);// editVM deki OrderId ile order ın ID sini eşle demek istedik***

                order.TableID = employeeEditVM.TableId;
                order.Table = _db.Tables.FirstOrDefault(x => x.TableNo == tableNo);

                _db.Entry(order).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();


                Product product = _db.Products.FirstOrDefault(x => x.ID == productName);// productName deki OrderId ile product ın ID sini eşle demek istedik***

                OrderDetail orderDetail = new OrderDetail()// burada kullanıcıdan alınan değerleri tabloya atarız.*****
                {
                    ID = employeeEditVM.OrderDetailId,
                    Order = order,
                    OrderId = order.ID,
                    OrderDate = employeeEditVM.OrderDate,
                    Product = product,
                    ProductId = product.ID,
                    Quantity = employeeEditVM.Quantity,
                    UnitPrice = employeeEditVM.UnitPrice
                };

                product.OrderDetails.Add(orderDetail);
                order.OrderDetails.Add(orderDetail);
                _db.Entry(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(employeeEditVM);
            }
        }







        // GET: EmployeeController/Delete/5

        public ActionResult Delete(int id)

        {

            OrderDetail orderDetail = _db.OrderDetails.First(x => x.ID == id);

            EmployeeDeleteVM employeeDeleteVM = new EmployeeDeleteVM()// burada tablodaki değerleri vm tabloya atarız.*****
            {
                Id = orderDetail.ID,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                OrderDate = orderDetail.OrderDate,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice
            };

            return View(employeeDeleteVM);
        }



        // POST: EmployeeController/Delete/5

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Delete(EmployeeDeleteVM employeeDeleteVM)

        {

            try

            {
                OrderDetail orderDetail = _db.OrderDetails.First(x => x.ID == employeeDeleteVM.Id);
                _db.OrderDetails.Remove(orderDetail);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex)

            {
                ViewBag.Error = ex.Message;
                return View(employeeDeleteVM);

            }

        }
    }
}
