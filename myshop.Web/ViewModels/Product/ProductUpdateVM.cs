using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace myshop.Web.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public string? ImgPath { get; set; }

        [Display(Name = "Image")]
        public IFormFile? Img { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [Range(1, int.MaxValue)]
        public int? CategoryId { get; set; }
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
    }
}
