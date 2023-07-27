using EsyaCekilisV3.Web.Areas.Admin.Models;
using EsyaCekilisV3.Web.Migrations;
using EsyaCekilisV3.Web.Models;
using EsyaCekilisV3.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsyaCekilisV3.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        
        private readonly UserManager<AppUser> _userManager;
        private readonly ProductModel _productModel;
        

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
          
        }

        public IActionResult Index()
        {


            return View();
        }
        



        public async Task<IActionResult> UserList() 
        {
            var userList = await _userManager.Users.ToListAsync();

            var userViewModelList = userList.Select(x => new Models.UserViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.UserName
            }).ToList();
             
            return View(userViewModelList);
        }

    }
}
