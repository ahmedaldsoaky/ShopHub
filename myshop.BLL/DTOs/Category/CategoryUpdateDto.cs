
namespace myshop.BLL.DTOs.Category
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

    }
}
