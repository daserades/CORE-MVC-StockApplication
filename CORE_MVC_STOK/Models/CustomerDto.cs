using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_MVC_STOK.Models
{
    public class CustomerDto
    {
        public List<int> UrunId { get; set; }

        public HelperDto HelperDto { get; set; }
        public CustomerDto()
        {

        }
        public CustomerDto(Category category)
        {
            ObjectMapper.Map(this, category);
            UrunId = category.Products.Select(x => x.ProductId).ToList();
            HelperDto = ObjectMapper.Map<HelperDto>(category.Customer);
        }
    }
}
