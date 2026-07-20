using Microsoft.AspNetCore.Mvc.Rendering;
using myshop.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace myshop.Web.ViewModels.Product
{
    public class ProductCreateVM
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;
        
        [Required, MaxLength(200)]
        public string Description { get; set; } = null!;
        
        [Display(Name = "Image")]
        //[AllowedExtensions(".jpg", ".jpeg", ".png", ".webp")] نبقى نعملها بعدين
        public IFormFile? ImgPath { get; set; } = null!;
        
        [Required, Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        [Required, Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
            = Enumerable.Empty<SelectListItem>();
    }
}
