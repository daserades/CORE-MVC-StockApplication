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
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
    }
}
