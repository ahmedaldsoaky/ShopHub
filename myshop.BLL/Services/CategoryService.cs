using AutoMapper;
using myshop.BLL.DTOs.Category;
using myshop.BLL.Interfaces;
using myshop.DAL.Interfaces;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
        }


        public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(isTracking: false);
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            return category is null ? null : _mapper.Map<CategoryReadDto>(category);
        }

        public async Task Update(CategoryUpdateDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(dto.Id);

            if (category is null)
                throw new KeyNotFoundException("Category not found.");

            _mapper.Map(dto, category);

            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            if (category is null)
                return;

            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.SaveAsync();
        }

    }
}
