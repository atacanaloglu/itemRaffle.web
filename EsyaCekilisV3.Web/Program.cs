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

//securitystampi biz update ediyoruz �nemli bir de�i�iklik yapt���mzda.Concurrencystamp de�eri ise en ufak de�i�iklikte kendini s�rekli otomatik g�nceller.

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(30); //stock hali zaten. 30 dakikada bir kullan�c�n�n security stampini kontrol eder. kullan�c� biligilerindew de�i�iklik oldu�unda bu data update alacak.
});


builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddIdentityWithExt();
builder.Services.AddScoped<IEmailServise, EmailService>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "EsyaAppCookie"; //cookie ismi
    opt.LoginPath = new PathString("/Home/Signin");//kullan�c� giri� yapmadan �yelere �zel bir yere gitmek isterse y�nlendirecek
    opt.LogoutPath = new PathString("/Member/logout");
    opt.AccessDeniedPath = new PathString("/Member/AccessDenied");
    opt.Cookie = cookieBuilder;
    opt.ExpireTimeSpan = TimeSpan.FromDays(60); //cookie muhafaza s�resi
    opt.SlidingExpiration = true; //bu her giri�te 60 g�nl�k cookie �mr� refresh i�in
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

app.UseAuthentication(); //kimlik do�rulama (google ile giri� yap gibi �eylerde laz�m)

app.UseAuthorization(); //kimlik yetkilendirme (kimlik bazl� uygulamalarda laz�m)

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
