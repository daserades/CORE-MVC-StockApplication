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
        public int CustomerId { get; set; }
        [Column("nvarchar(50)")]
        public string CustomerName { get; set; }
        [Column("nvarchar(50)")]
        public string CustomerSurname { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }

    }
}
