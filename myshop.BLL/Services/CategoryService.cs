using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using myshop.BLL.DTOs.Category;
using myshop.BLL.DTOs.Product;
using myshop.BLL.Interfaces;
using myshop.BLL.Mapping.Projections;
using myshop.Common;
using myshop.DAL.Interfaces;
using myshop.DAL.UnitOfWork;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private const string CategoriesCacheKey = "Categories";

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
        {
            // _unitOfWork.Categories.GetProjectedAsync(CategoryProjection.ToReadDto, pageSize: int.MaxValue);
            return await _cache.GetOrCreateAsync(CategoriesCacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

                return await _unitOfWork.Categories.GetProjectedAsync(
                    CategoryProjection.ToReadDto,
                    pageSize: int.MaxValue);
            });
        }

        public async Task<PagedResult<CategoryReadDto>> GetPagedAsync(DataTableRequestDto? requestDto)
        {
            Expression<Func<Category, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(requestDto?.Search))
            {
                string searchTerm = requestDto.Search.Trim();
                filter = c =>
                        c.Name.Contains(searchTerm) ||
                        c.Description.Contains(searchTerm) ||
                        c.CreatedTime.ToString().Contains(searchTerm);
            }

            Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null;

            switch (requestDto?.SortColumn?.ToLower())
            {
                case "name":
                    orderBy = requestDto.SortDirection == "desc" ?
                        q => q.OrderByDescending(c => c.Name) :
                        q => q.OrderBy(c => c.Name);
                    break;
                case "description":
                    orderBy = requestDto.SortDirection == "desc" ?
                        q => q.OrderByDescending(c => c.Description) :
                        q => q.OrderBy(c => c.Description);
                    break;
                case "createdtime":
                    orderBy = requestDto.SortDirection == "desc" ?
                        q => q.OrderByDescending(c => c.CreatedTime) :
                        q => q.OrderBy(c => c.CreatedTime);
                    break;
                default:
                    orderBy = q => q.OrderBy(p => p.Id);
                    break;
            };

            var categories = await _unitOfWork.Categories.GetProjectedAsync<CategoryReadDto>(
                selector: CategoryProjection.ToReadDto,
                pageNumber: requestDto.PageNumber,
                pageSize: requestDto.PageSize,
                filter: filter,
                orderBy: orderBy
            );
            return new PagedResult<CategoryReadDto>
            {
                Data = categories,
                TotalCount = await _unitOfWork.Categories.CountAsync(),
                FilteredCount = await _unitOfWork.Categories.CountAsync(filter),
            };
        }

        public async Task<CategoryReadDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetProjectedFirstOrDefaultAsync(
                CategoryProjection.ToReadDto,
                c => c.Id == (int)id
            );
            return category;
        }

        public async Task<bool> Exists(Expression<Func<Category, bool>> expr)
            => await _unitOfWork.Categories.ExistsAsync(expr);

        public async Task<int> CountAsync(Expression<Func<Category, bool>>? filter = null)
            => await _unitOfWork.Categories.CountAsync(filter);

        public async Task AddAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            _cache.Remove(CategoriesCacheKey);
        }

        public async Task Update(CategoryUpdateDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(dto.Id);

            if (category is null)
                throw new KeyNotFoundException("Category not found.");

            _mapper.Map(dto, category);

            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveAsync();
            _cache.Remove(CategoriesCacheKey);
        }

        public async Task Delete(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            if (category is null)
                return;

            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.SaveAsync();
            _cache.Remove(CategoriesCacheKey);
        }

        
    }
}
