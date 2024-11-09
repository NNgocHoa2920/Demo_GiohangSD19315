using Demo_GiohangSD19315.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GHDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//khai báo dịch vụ cho sesion
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromSeconds(15); // khai báo thời gian timeout của sesion là 15s
   // nếu người dùng không thực hành động tiếp theo trong 15s thì nó sẽ hết hạn
   //nesu trong khoảng 15 mà người dùng thực hiện thì hệ thống sẽ reset thời gan sesion

});
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
app.UseSession();// khai báo để sử dụng session

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
