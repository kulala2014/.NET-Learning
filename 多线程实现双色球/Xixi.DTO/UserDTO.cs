using System;
using Xixi.Common.XixiAttribute;

namespace Xixi.DTO
{
    public class UserDTO
    {
        [Name("名字")]
        public string Name { get; set; }
        [Required]
        [StrLength(10,20)]
        public string Account { get; set; }
        public string Password { get; set; }
        [Name("邮箱地址")]
        [Required]
        [Email]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? CompanyId { get; set; }
    }
}
