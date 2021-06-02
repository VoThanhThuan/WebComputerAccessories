using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebComputerAccessories.Models.ViewModel
{
    public class ProductsMV
    {
        public List<ProductsMV> ConvertMV(List<Product> request)
        {
            return request.Select(item => new ProductsMV()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Stock = item.Stock,
                    DateCreated = item.DateCreated,
                    Image = item.Image,
                    Details = item.Details,
                    IdCategory = item.IdCategory
                })
                .ToList();
        }
        public string Json { get; set; }


        public Guid Id { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public int? Stock { get; set; }

        public DateTime? DateCreated { get; set; }

        public string Image { get; set; }

        public string Details { get; set; }

        public int? IdCategory { get; set; }
    }

}