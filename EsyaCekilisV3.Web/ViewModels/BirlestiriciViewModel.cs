using EsyaCekilisV3.Web.Areas.Admin.Models;
using EsyaCekilisV3.Web.Models;

namespace EsyaCekilisV3.Web.ViewModels
{
    public class BirlestiriciViewModel
    {
        public ProductModel Product { get; set; }
        public SaleModel Sale { get; set; }
        public AppUser AppUser { get; set; }
    }
}
