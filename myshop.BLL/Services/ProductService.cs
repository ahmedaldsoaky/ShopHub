using AutoMapper;
using myshop.BLL.DTOs.Product;
using myshop.BLL.Helpers;
using myshop.BLL.Interfaces;
using myshop.BLL.Mapping.Projections;
using myshop.Common;
using myshop.DAL.Interfaces;
using myshop.Entities.Models;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace myshop.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageValidationService _imageValidationService;
        private readonly IImageService _imageService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IImageValidationService imageValidationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageValidationService = imageValidationService;
        }


        #region Admin


        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
            => await _unitOfWork.Products.GetProjectedListAsync(ProductProjection.ToReadDto);

        public async Task<PagedResult<ProductReadDto>> GetPagedAsync(PagedRequestDto? requestDto)
        {
            Expression<Func<Product, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(requestDto?.Search))
                filter = BuildSearchFilter(requestDto.Search);

            var orderBy = 
                ProductSorting
                    .GetSorting(requestDto?.SortColumn, requestDto?.SortDirection);

            var products = await _unitOfWork.Products
                .GetProjectedAsync(
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

                PageNumber = requestDto.PageNumber,
                PageSize = requestDto.PageSize
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

            if (!string.IsNullOrWhiteSpace(dto.ImageFileName))
            {
                var extension = Path.GetExtension(dto.ImageFileName);
                
                if (!_imageValidationService.IsValid(extension, dto.ImageSize))
                    throw new ArgumentException("Invalid image file.");
                
                var imagePath = await _imageService.SaveAsync(
                    dto.ImageFileName,
                    dto.ImageContent,
                    "Products");
                product.ImgPath = imagePath;
            }
            
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(ProductUpdateDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(dto.Id);
            
            if (product is null)
                throw new KeyNotFoundException("Product not found.");
            
            var oldImagePath = product.ImgPath;
            
            _mapper.Map(dto, product);
            
            if (!string.IsNullOrWhiteSpace(dto.ImageFileName))
            {
                var extension = Path.GetExtension(dto.ImageFileName);

                if (!_imageValidationService.IsValid(
                        extension,
                        dto.ImageSize))
                {
                    throw new ArgumentException("Invalid image file.");
                }    

                product.ImgPath = await _imageService.ReplaceAsync(
                    fileName     : dto.ImageFileName,
                    content      : dto.ImageContent!,
                    oldImagePath : oldImagePath,
                    "Products");
            }

            // tracked object 
            // no need
            //_unitOfWork.Products.Update(dto);

            await _unitOfWork.SaveAsync();
        }
        public async Task Delete(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product is null)
                throw new KeyNotFoundException("Product not found.");

            _imageService.Delete(product.ImgPath);
            
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveAsync();
        }

        #endregion


        private static Expression<Func<Product, bool>>? BuildSearchFilter(string search)
        {
            search = search.Trim();
            return p =>
                p.Name.Contains(search) ||
                p.Category!.Name.Contains(search) ||
                p.Description.Contains(search);
        }

        #region Customer

        public Task<IReadOnlyList<ProductReadDto>> GetLatestProductsAsync(int count)
        {
            return _unitOfWork.Products.GetProjectedAsync(
                ProductProjection.ToReadDto,
                pageNumber: 1,
                pageSize: count,
                orderBy: q => q.OrderByDescending(p => p.Id)
            );
        }

        public Task<IReadOnlyList<ProductReadDto>> GetRelatedProductsAsync(int categoryId, int productId, int count = 4)
        {
            return _unitOfWork.Products.GetProjectedAsync(
                ProductProjection.ToReadDto,
                pageNumber: 1,
                pageSize: count,
                filter: p => p.CategoryId == categoryId && p.Id != productId,
                orderBy: q => q.OrderBy(p => p.Name)
            );
        } 
        
        #endregion


    }
}
