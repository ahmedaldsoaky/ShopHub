using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTOs.Category
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

    }
}
