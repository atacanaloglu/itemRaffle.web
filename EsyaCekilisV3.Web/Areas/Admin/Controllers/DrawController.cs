using EsyaCekilisV3.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EsyaCekilisV3.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DrawController : Controller
    {
        public IActionResult Index()
        {
            // todo: endate < datetime.now && owneduser == null
            return View();
        }

        public IActionResult DrawResult(ProductModel product, SaleModel sale)
        {


            // TODO: productId ile kazanan userID belirle
            int[] users = new int[] { 1, 2 , 3 }; // todo: sales tablosunda product userları
            Random rand = new Random();
            int winner = rand.Next(users.Length);
            // users[winner];
            // todo: producs tablosunda owner olarak update et
            return View();
        }
    }
}
