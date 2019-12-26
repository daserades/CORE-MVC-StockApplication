using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_MVC_STOK.Models
{
    public class Customer
    {
        public Customer()
        {
            Sales = new HashSet<Sales>();
        }
        [Key]
        [Display(Name ="Müşteri ID")]
        public int CustomerId { get; set; }
        [Display(Name ="Müşteri Ad")]
        [Required(ErrorMessage ="Müşteri adı boş bırakılamaz !")]
        public string CustomerName { get; set; }
        [Display(Name ="Müşteri Soyad")]
        [Required(ErrorMessage = "Müşteri soyadı boş bırakılamaz !")]
        public string CustomerSurname { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
