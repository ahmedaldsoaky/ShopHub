using myshop.BLL.DTOs.Product;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllAsync();
        Task<ProductReadDto?> GetByIdAsync(int id);
        Task AddAsync(ProductCreateDto dto);
        Task Update(ProductUpdateDto dto);
        Task Delete(int id);
    }
}
