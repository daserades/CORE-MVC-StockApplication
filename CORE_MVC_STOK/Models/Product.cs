using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_MVC_STOK.Models
{
    public class Product
    {
        public Product()
        {
            Sales = new HashSet<Sales>();
        }
        [Key]
        [Display(Name ="Ürün Id")]
        public int ProductId { get; set; }
        [Display(Name ="Ürün Adı")]
        [Required(ErrorMessage ="Ürün Adı boş bırakılamaz.")]
        public string ProductName { get; set; }
        [Display(Name ="Ürün Markası")]
        [Required(ErrorMessage = "Ürün MArkası boş bırakılamaz.")]
        public string ProductBrand { get; set; }
        [Display(Name ="Ürün Fiyat")]
        [Required(ErrorMessage = "Ürün Fiyat boş bırakılamaz.")]
        public decimal ProductPrice { get; set; }
        [Display(Name ="Ürün Stok")]
        [Required(ErrorMessage = "Ürün Stok boş bırakılamaz.")]
        public int ProductStock { get; set; }
        [Display(Name ="Kategori Adı")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
    }
}
