using Azure.Core;
using EsyaCekilisV3.Web.Areas.Admin.Models;
using EsyaCekilisV3.Web.Extenisons;
using EsyaCekilisV3.Web.Models;
using EsyaCekilisV3.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EsyaCekilisV3.Web.Controllers
{

    [Authorize] //authorize kullandıktan sonra artık burada bir sayfa yapılabilir
    public class MemberController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _appDbContext;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, AppDbContext appDbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()//member index sayfası
        {

            var currentUser =await _userManager.FindByNameAsync(User.Identity!.Name!);
            
            var userViewModel= new ViewModels.UserViewModel { Email= currentUser!.Email,UserName=currentUser.UserName, PhoneNumber= currentUser.PhoneNumber };

            return View(userViewModel);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

        }
        public IActionResult PasswordChange()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, request.PasswordOld);

            if (!checkOldPassword)
            {
                ModelState.AddModelError(string.Empty, "Şifre yanlış.");
            }

            var resultChangePassword = await _userManager.ChangePasswordAsync(currentUser, request.PasswordOld,request.PasswordNew);

            if(!resultChangePassword.Succeeded) 
            {
                ModelState.AddModelErrorList(resultChangePassword.Errors.Select(x=>x.Description).ToList());
                return View();
            }


            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, request.PasswordNew, true,false);  

            TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirilmiştir.";



            return View();

        }


        public async Task<IActionResult> AccessDenied(string ReturnUrl)
        {

            string message = string.Empty;

            message = "Bu sayfayı görmeye yetkiniz yoktur. Yetki almak için yöneticiniz ile görüşebilirsiniz.";
            ViewBag.message=message;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> CheckOut (string request) 
        {
            
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            foreach (var productId in request.Split(','))
            {
                SaleModel model = new();
                model.ProductId = Convert.ToInt32(productId);
                model.UserId = currentUser.Id;
                model.UserEmail = currentUser.Email;
                _appDbContext.SaleModels.Add(model);

                _appDbContext.ProductModels.Find(model.ProductId).CouponLimit--;
            }


            _appDbContext.SaveChanges();
            

            return View();
        }

        public IActionResult myproducts()
        {
            string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            List<ProductModel> productlist = _appDbContext.ProductModels.Where(p => p.OwnedUser == userid).ToList();
            return View(productlist);
        }

        public IActionResult myorders()
        {

            string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            List<SaleModel> productlist = _appDbContext.SaleModels.Include(i => i.Product).Where(p => p.UserId == userid).ToList();
            return View(productlist);
        }
    }
}
