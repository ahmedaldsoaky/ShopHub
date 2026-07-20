using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Description { get; set; }

        [DisplayName("Image")]
        public string? ImgPath { get; set; }

        public required decimal Price { get; set; }

        [DisplayName("Category")]
        public required int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
