using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTOs.Product
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string? ImageFileName { get; set; }
        public long ImageSize { get; set; }
        public Stream? ImageContent { get; set; }

        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
