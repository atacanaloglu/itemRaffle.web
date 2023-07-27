using Microsoft.AspNetCore.Identity;

namespace EsyaCekilisV3.Web.Localizations
{
    //Türkçeleştirmeler (hata dönüşleri)
    public class LocalizationIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code = "DuplicateUsername",Description=$"{userName} kullanıcı ismi başkası tarafından kullanılmış" };
           
            //return base.DuplicateUserName(userName); bu stock olanı
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DuplicateEmail", Description = $"Bu email adresine kayıtlı hesap bulunmaktadır. " }; 
                //return base.DuplicateEmail(email);
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort", Description = $"Şifre 6 haneden az olamaz." };
            //return base.PasswordTooShort(length);
        }


    }
}
