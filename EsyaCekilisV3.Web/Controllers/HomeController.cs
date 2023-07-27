using EsyaCekilisV3.Web.Models;
using EsyaCekilisV3.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EsyaCekilisV3.Web.Extenisons;
using NuGet.Common;
using EsyaCekilisV3.Web.Services;
using EsyaCekilisV3.Web.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace EsyaCekilisV3.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailServise _emailServise;
        private readonly AppDbContext _appDbContext;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailServise emailServise, AppDbContext appDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailServise = emailServise;
            _appDbContext = appDbContext;
        }

        public IActionResult Index(int id)
        {
            List<ProductModel> productlist = _appDbContext.ProductModels.ToList();
            return View(productlist);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {

            return View();
        } 

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> SignIn(SignInViewModel model,string? returnUrl=null)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }


            returnUrl = returnUrl ?? Url.Action("Index", "Home");
            var hasUser= await _userManager.FindByEmailAsync(model.Email);
          
            if(hasUser == null) 
            {
                ModelState.AddModelError(string.Empty, "Email veya şifre yanlış");
                return View();
            }
            var signInresult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, false);

            if(signInresult.Succeeded)
            {
                return Redirect(returnUrl!);
            }

            ModelState.AddModelErrorList(new List<string>() { "Email veya şifre yanlış" });

           
            return View();

        }

        [HttpPost]

        public async Task<IActionResult> SignUp(SignUpViewModel request) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var IdentityResult= await  _userManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.Phone, Email = request.Email },request.PasswordConfirm);


            if(IdentityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Üyelik Kayıt İşlemi Başarıyla Gerçekleşmiştir.";

                return RedirectToAction(nameof(HomeController.SignUp));
            }

            ModelState.AddModelErrorList(IdentityResult.Errors.Select(x => x.Description).ToList());

            return View();
        }


        public IActionResult ForgetPassword()
        {


            return View();
        }

        [HttpPost]

        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            var hasUser= await _userManager.FindByEmailAsync(request.Email);

            if (hasUser == null) 
            {
                ModelState.AddModelError(string.Empty, "Bu email adresine sahip kullanıcı bulunamadı");
                return View();
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            var passwordResetLink = Url.Action("ResetPassword", "Home", new {userId=hasUser.Id,Token=passwordResetToken},HttpContext.Request.Scheme);
            // gibi bir linkhttps://localhost:7004?userId=12213&token=asgakjf

            await _emailServise.SendResetPasswordEmail(passwordResetLink!,hasUser.Email!);

            TempData["SuccessMessage"] = "Şifre yenileme linki email adresinize gönderildi";
            return RedirectToAction(nameof(ForgetPassword)); //sayfa refresh durumunda tekrar tekrar mail yollamasın diye sayfanın başına geri yolluyoruz
        }

        public IActionResult ResetPassword(string userId,string token) //mesela bu ilk gelen o sayfayı ilgilendiriyor
        {
            TempData["userId"] = userId;
            TempData["token"] = token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request) //button a basınca da bu action method çalışacak
        {
            var userId= TempData["userId"];
            var token= TempData["token"];

            if (userId == null || token==null) 
            {
                throw new Exception("Bir hata meydana geldi.");
            }

            var hasUser = await _userManager.FindByIdAsync(userId.ToString()!);

            if (hasUser == null) 
            {   
                ModelState.AddModelError(String.Empty, "Kullanıcı Bulunamamıştır");
                return View();
            }

            IdentityResult result= await _userManager.ResetPasswordAsync(hasUser,token.ToString()!,request.Password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla yenilenmiştir.";
            }
            else
            {
                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
              
            }


            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}