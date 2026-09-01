using myshop.BLL.DTOs.Product;
using myshop.BLL.DTOs.User;
using myshop.Common;
using System.Linq.Expressions;

namespace myshop.BLL.Interfaces
{
    public interface IBaseService<TDto, TEntity, TKey>
    {
        Task<PagedResult<TDto>> GetPagedAsync(PagedRequestDto? requestDto);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(TKey id);

        Task<bool> Exists(Expression<Func<TEntity, bool>> expr);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
    }
}
