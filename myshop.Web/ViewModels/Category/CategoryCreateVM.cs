namespace myshop.Web.ViewModels.Category
{
    public class CategoryCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

    }
}
