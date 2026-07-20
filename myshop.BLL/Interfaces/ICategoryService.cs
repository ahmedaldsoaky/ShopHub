using myshop.BLL.DTOs.Category;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDto>> GetAllAsync();
        Task<CategoryReadDto?> GetByIdAsync(int id);
        Task AddAsync(CategoryCreateDto dto);
        Task Update(CategoryUpdateDto dto);
        Task Delete(int id);
    }
}
