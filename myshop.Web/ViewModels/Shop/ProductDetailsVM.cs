using myshop.BLL.DTOs.Product;

namespace myshop.Web.ViewModels.Shop
{
    public class ProductDetailsVM
    {
        public ProductReadDto Product { get; set; } = default!;

        public IReadOnlyList<ProductReadDto> RelatedProducts { get; set; }
            = [];
    }
}
