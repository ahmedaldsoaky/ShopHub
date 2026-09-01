using myshop.BLL.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<IReadOnlyList<OrderReadDto>> GetUserOrdersAsync(string userId);
    }
}
