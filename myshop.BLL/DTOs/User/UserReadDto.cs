using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTOs.User
{
    public class UserReadDto
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public bool IsLocked { get; set; }
    }
}
