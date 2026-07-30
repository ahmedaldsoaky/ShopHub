using AutoMapper;
using myshop.BLL.DTOs.Product;
using myshop.BLL.Interfaces;
using myshop.BLL.Mapping.Projections;
using myshop.Common;
using myshop.DAL.Interfaces;
using myshop.Entities.Models;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
            => await _unitOfWork.Products.GetProjectedAsync(ProductProjection.ToReadDto, pageSize: int.MaxValue);

        public async Task<PagedResult<ProductReadDto>> GetPagedAsync(DataTableRequestDto? requestDto)
        {
            Expression<Func<Product, bool>>? filter = null;
            if(!string.IsNullOrWhiteSpace(requestDto?.Search))
            {
                var searchTerm = requestDto.Search.Trim();
                filter = p =>
                        p.Name.Contains(searchTerm) ||
                        p.Description.Contains(searchTerm) || 
                        p.Category.Name.Contains(searchTerm);
            }
            
            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null;

            switch (requestDto?.SortColumn?.ToLower())
            {
                case "name":
                    orderBy = requestDto.SortDirection == "desc"
                        ? q => q.OrderByDescending(p => p.Name)
                        : q => q.OrderBy(p => p.Name);
                    break;
                case "price":
                    orderBy = requestDto.SortDirection == "desc"
                        ? q => q.OrderByDescending(p => p.Price)
                        : q => q.OrderBy(p => p.Price);
                    break;
                case "categoryname":
                    orderBy = requestDto.SortDirection == "desc"
                        ? q => q.OrderByDescending(p => p.Category.Name)
                        : q => q.OrderBy(p => p.Category.Name);
                    break;
                case "description":
                    orderBy = requestDto.SortDirection == "desc"
                        ? q => q.OrderByDescending(p => p.Description)
                        : q => q.OrderBy(p => p.Description);
                    break;
                default:
                    orderBy = q => q.OrderBy(p => p.Id);
                    break;
            }
            
            var products = await _unitOfWork.Products.GetProjectedAsync<ProductReadDto>(
                selector: ProductProjection.ToReadDto,
                pageNumber: requestDto.PageNumber,
                pageSize: requestDto.PageSize,
                filter: filter,
                orderBy: orderBy
            );
            return new PagedResult<ProductReadDto>
            {
                Data = products,
                TotalCount = await _unitOfWork.Products.CountAsync(),
                FilteredCount = await _unitOfWork.Products.CountAsync(filter),
            };
        }

        public async Task<ProductReadDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products
                .GetProjectedFirstOrDefaultAsync(
                ProductProjection.ToReadDto,
                p => p.Id == id);
            return product;
        }

        public async Task<bool> Exists(Expression<Func<Product, bool>> expr)
        {
            return await _unitOfWork.Products.ExistsAsync(expr);
        }

        public async Task<int> CountAsync(Expression<Func<Product, bool>>? filter = null)
        {
            return await _unitOfWork.Products.CountAsync(filter);
        }


        public async Task AddAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(ProductUpdateDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(dto.Id);
            if (product is null)
                throw new KeyNotFoundException("Product not found.");
            _mapper.Map(dto, product);
            
            // tracked object 
            // no need
            //_unitOfWork.Products.Update(dto);
            
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
