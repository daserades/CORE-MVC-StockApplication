using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_MVC_STOK.Models
{

    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }


        [Key]
        [Display(Name ="Kategori ID")]
        public int CategoryId { get; set; }
        [Display(Name ="Kategori Adı")]
        [Required(ErrorMessage ="Kategori Adı Boş bırakılamaz !")]
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
