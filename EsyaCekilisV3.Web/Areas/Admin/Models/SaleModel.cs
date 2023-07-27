using EsyaCekilisV3.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsyaCekilisV3.Web.Areas.Admin.Models
{

    public class SaleModel
    {

        public int Id { get; set; }  //bu id atanmazsa keyless oluyor. keyless olduğunda db işlemleri aksar. 
        
        public string UserId { get; set; }

        public string UserEmail { get; set; }
        
        public DateTime BuyDate { get; set; } = DateTime.Now;

        [ForeignKey("ProductModel")]
        public int ProductId { get; set; } 
        public ProductModel Product { get; set; }
    }


}
