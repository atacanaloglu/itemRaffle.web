using EsyaCekilisV3.Web.Areas.Admin.Models;
using EsyaCekilisV3.Web.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EsyaCekilisV3.Web.Models
{
    public class AppDbContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)   {  }
        
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<SaleModel> SaleModels { get; set; }
        
        
        
    }
}
