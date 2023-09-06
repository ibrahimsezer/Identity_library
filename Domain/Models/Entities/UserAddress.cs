using Microsoft.AspNetCore.Identity;

namespace Identity_library.Domain.Models.Entities
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }

    }
}
