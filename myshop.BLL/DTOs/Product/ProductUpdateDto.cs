
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace myshop.BLL.DTOs.Product
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        public string? ImageFileName { get; set; }
        public long ImageSize { get; set; }
        public Stream? ImageContent { get; set; }

        public decimal? Price { get; set; }
        
        public int? CategoryId { get; set; }
    }
}
