using EsyaCekilisV3.Web.CustomValidations;
using EsyaCekilisV3.Web.Localizations;
using EsyaCekilisV3.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace EsyaCekilisV3.Web.Extenisons
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExt(this IServiceCollection servises)
        {

            servises.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(2);
            });

            servises.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true; //Username stock olarak unique zaten ona gerek yok
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

            }).AddErrorDescriber<LocalizationIdentityErrorDescriber>() 
              .AddPasswordValidator<PasswordValidator>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

        }
    }
}
