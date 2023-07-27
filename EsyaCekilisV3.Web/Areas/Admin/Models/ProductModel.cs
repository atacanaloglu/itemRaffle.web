using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EsyaCekilisV3.Web.Areas.Admin.Models
{

    public class ProductModel : AsSerializeable
    {

        public int Id { get; set; } //bu id atanmazsa keyless oluyor. keyless olduğunda db işlemleri aksar.
       
        public string CreatedUser { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Kupon ismi alanı boş bırakılamaz.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Kupon Açıklaması alanı boş bırakılamaz.")]
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DrawDate { get; set; }
        [Required(ErrorMessage = "Kupon Sayısı alanı boş bırakılamaz.")]
        public int? CouponLimit { get; set; }
        [Required(ErrorMessage = "Fiyat alanı boş bırakılamaz.")]
        public decimal? Price { get; set; }
        public string? OwnedUser { get; set; }
        public string? OwnedUserName { get; set; }
    }


}
