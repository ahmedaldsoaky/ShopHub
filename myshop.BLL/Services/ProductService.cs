using AutoMapper;
using myshop.BLL.DTOs.Product;
using myshop.BLL.Interfaces;
using myshop.DAL.Interfaces;
using myshop.Entities.Models;

namespace myshop.BLL.Services
{
    public class ProductService : IProductService
    {
        //private string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync(
                includes: [p => p.Category]
                , isTracking: false);
            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        public async Task<ProductReadDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdWithCategoryAsync(id);
            return product is null ? null : _mapper.Map<ProductReadDto>(product);
        }

        public async Task Update(ProductUpdateDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdWithCategoryAsync(dto.Id);
            if (product is null)
                throw new KeyNotFoundException("Product not found.");
            var updatedProduct = _mapper.Map(dto, product);
            _unitOfWork.Products.Update(updatedProduct);
            await _unitOfWork.SaveAsync();
        }
        public async Task Delete(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product is null)
                return;
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveAsync();
        }
    }
}
