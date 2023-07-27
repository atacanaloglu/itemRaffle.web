using EsyaCekilisV3.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace EsyaCekilisV3.Web.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors= new List<IdentityError>();
            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new() { Code = "PasswordContainUserName", Description = "Şifre Alanı Kullanıcı Adı İçeremez" });
            }
            if(errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray())); 
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}

