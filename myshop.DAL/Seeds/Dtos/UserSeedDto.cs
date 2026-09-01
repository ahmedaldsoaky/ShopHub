using System;
using System.Collections.Generic;
namespace myshop.DAL.Seeds.Dtos
{
    public class UserSeedDto
    {
        public required string FullName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Role { get; set; }
    }
}
