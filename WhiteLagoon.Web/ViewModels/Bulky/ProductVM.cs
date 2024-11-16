using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Domain.Entities.Bulky;

namespace WhiteLagoon.Web.ViewModels.Bulky
{
    public class ProductVM
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        //[ValidateNever]
        //public List<ProductImage> ProductImages { get; set; }
    }
}
