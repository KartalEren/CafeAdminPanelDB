using CafeAdminPanelDB.CustomActionFilter;
using CafeAdminPanelDB.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(new CustomActionFilter("Global"));
//});//EÐER BURASI AKTÝF OLSAYDI KULLANICILAR DÝREKT GÝRÝÞ EKRANINA YÖNLENDÝRÝLDÝ.AMA BÝZ BUNU OLSUN ÝSTEMÝYORUZ.


builder.Services.AddDbContext<AdminPanelDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BaglantiAdi"))); //DATABASEYÝ BURADA NEW LÝYORUZ.

builder.Services.AddSession();//kýsa süreli bilgi tutan kýsýmdýr. cookie benzeridir ama cookie deðildir. Default 20 dk dýr. ****************Bankacýlýk uygulamasýnda hiç bir iþlem yapýlmazsa otomatik atan uygulamadýr.**********



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();
