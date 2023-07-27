using Azure.Core;
using EsyaCekilisV3.Web.Areas.Admin.Models;
using EsyaCekilisV3.Web.Extenisons;
using EsyaCekilisV3.Web.Models;
using EsyaCekilisV3.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Data.Common;
using System.Security.Claims;

namespace EsyaCekilisV3.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class ProductController : Controller
    {
       
        
        private readonly AppDbContext _appDbContext; //AppDbContext classından bir nesne oluşturduk. Bu nesne ile modellere erişiyoruz.
        private readonly UserManager<AppUser> userManager;

        public ProductController(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            this.userManager = userManager;
        }

        public IActionResult ProductIndex()
        {
            List<ProductModel> productlist = _appDbContext.ProductModels.ToList();
            return View(productlist);
        }

        public IActionResult deleteCoupon(int id)
        {
            var couponToDelete = _appDbContext.ProductModels.Find(id);

            if (couponToDelete != null)
            {
                _appDbContext.ProductModels.Remove(couponToDelete);
                _appDbContext.SaveChanges();
            }

            return RedirectToAction("ProductIndex"); // Silme işlemi tamamlandıktan sonra başka bir sayfaya yönlendirilebilirsiniz.
        }


        public IActionResult createCoupon(int id)
        {
            var userName = User.Identity.Name;

            ProductModel product = _appDbContext.ProductModels.Find(id) ?? new ProductModel()
            {
                CreatedUser = userName
            };

            // Veritabanına kaydetme işlemi
            //_appDbContext.ProductModels.Add(product);
            _appDbContext.SaveChanges();

            return View(product);
        }
        [HttpPost]
        public IActionResult createCoupon(ProductModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (request.Id > 0)
            {
                var p = _appDbContext.ProductModels.Find(request.Id);
                _appDbContext.Entry(p).CurrentValues.SetValues(request);
            }
            else 
            {
                _appDbContext.ProductModels.Add(request);
            }
            
            _appDbContext.SaveChanges();
            ViewData["SuccessMessage"] = "İşlem Gerçekleşmiştir.";

            return View();
        }

        public IActionResult drawCoupon () 
        {
            
            List<ProductModel> productlist = _appDbContext.ProductModels.ToList();
            return View(productlist);
        }

        [HttpPost]
        public async Task<IActionResult> drawCoupon(int productId, string action)
        {
            if (action == "delete")
            {
                var couponToDelete = _appDbContext.ProductModels.Find(productId);
                couponToDelete.OwnedUser = null;
                couponToDelete.OwnedUserName = null;

                _appDbContext.SaveChanges();
            } 
            else
            {
                List<SaleModel> sales = _appDbContext.SaleModels.Where(s => s.ProductId == productId).ToList();
                var index = new Random().Next(sales.Count);
                var winner = sales[index];
                var product = _appDbContext.ProductModels.Find(winner.ProductId);
                product.OwnedUser = winner.UserId;
                var user = await userManager.FindByIdAsync(winner.UserId);
                product.OwnedUserName = $"{user.UserName} <{user.Email}>";
                _appDbContext.SaveChanges();
            }

            return RedirectToAction("drawCoupon", "Product");
        }

        

    }
}
