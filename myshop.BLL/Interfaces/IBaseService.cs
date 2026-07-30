using myshop.BLL.DTOs.Product;
using myshop.BLL.DTOs.User;
using myshop.Common;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Interfaces
{
    public interface IBaseService<TDto, TEntity, TKey>
    {
        Task<PagedResult<TDto>> GetPagedAsync(DataTableRequestDto? requestDto);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(TKey id);

        Task<bool> Exists(Expression<Func<TEntity, bool>> expr);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
    }
}
