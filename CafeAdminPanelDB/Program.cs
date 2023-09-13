using CafeAdminPanelDB.CustomActionFilter;
using CafeAdminPanelDB.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(new CustomActionFilter("Global"));
//});//E�ER BURASI AKT�F OLSAYDI KULLANICILAR D�REKT G�R�� EKRANINA Y�NLEND�R�LD�.AMA B�Z BUNU OLSUN �STEM�YORUZ.


builder.Services.AddDbContext<AdminPanelDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BaglantiAdi"))); //DATABASEY� BURADA NEW L�YORUZ.

builder.Services.AddSession();//k�sa s�reli bilgi tutan k�s�md�r. cookie benzeridir ama cookie de�ildir. Default 20 dk d�r. ****************Bankac�l�k uygulamas�nda hi� bir i�lem yap�lmazsa otomatik atan uygulamad�r.**********



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
