using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_MVC_STOK.Models
{
    public class Sales
    {
        [Key]
        [Display(Name = "Satış Numarası")]
        public int SaleId { get; set; }
        [Display(Name = "Adet")]
        public int Piece { get; set; }
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Müşteri Adı")]
        public int CustomerId { get; set; }
        [Display(Name = "Ürün")]
        public int ProductId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
