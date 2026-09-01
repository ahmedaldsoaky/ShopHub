using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Interfaces
{
    public interface IImageValidationService
    {
        bool IsValid(string extension, long size);
    }
}
