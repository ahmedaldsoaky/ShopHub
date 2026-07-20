using System.ComponentModel.DataAnnotations;

namespace myshop.Entities.Models
{
    public class Category
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }
        public required DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
