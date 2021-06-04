using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebComputerAccessories.Models.ViewModel
{
    public class ProductMV
    {
        public List<ProductMV> ConvertMV(List<Product> request)
        {
            return request.Select(item => new ProductMV()
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
        public Product ConvertOrigin(ProductMV request)
        {
            return new Product()
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                DateCreated = request.DateCreated,
                Image = request.Image,
                Details = request.Details,
                IdCategory = request.IdCategory
            };

        }
        public string Json { get; set; }


        public Guid Id { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public int? Stock { get; set; }

        public DateTime? DateCreated { get; set; }

        public HttpPostedFileBase DataImage { get; set; }

        public string Image { get; set; }

        public string Details { get; set; }

        public int? IdCategory { get; set; }
    }

}