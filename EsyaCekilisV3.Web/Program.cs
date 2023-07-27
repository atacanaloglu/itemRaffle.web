using EsyaCekilisV3.Web.Models;
using Microsoft.EntityFrameworkCore;
using EsyaCekilisV3.Web.Extenisons;
using Microsoft.AspNetCore.Identity;
using EsyaCekilisV3.Web.OptionsModels;
using Microsoft.Extensions.DependencyInjection;
using EsyaCekilisV3.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

//securitystampi biz update ediyoruz önemli bir deðiþiklik yaptýðýmzda.Concurrencystamp deðeri ise en ufak deðiþiklikte kendini sürekli otomatik günceller.

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(30); //stock hali zaten. 30 dakikada bir kullanýcýnýn security stampini kontrol eder. kullanýcý biligilerindew deðiþiklik olduðunda bu data update alacak.
});


builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddIdentityWithExt();
builder.Services.AddScoped<IEmailServise, EmailService>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "EsyaAppCookie"; //cookie ismi
    opt.LoginPath = new PathString("/Home/Signin");//kullanýcý giriþ yapmadan üyelere özel bir yere gitmek isterse yönlendirecek
    opt.LogoutPath = new PathString("/Member/logout");
    opt.AccessDeniedPath = new PathString("/Member/AccessDenied");
    opt.Cookie = cookieBuilder;
    opt.ExpireTimeSpan = TimeSpan.FromDays(60); //cookie muhafaza süresi
    opt.SlidingExpiration = true; //bu her giriþte 60 günlük cookie ömrü refresh için
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

app.UseAuthentication(); //kimlik doðrulama (google ile giriþ yap gibi þeylerde lazým)

app.UseAuthorization(); //kimlik yetkilendirme (kimlik bazlý uygulamalarda lazým)

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
