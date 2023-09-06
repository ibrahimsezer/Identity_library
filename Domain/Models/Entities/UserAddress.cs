using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity_library.Domain.Models.Entities
{
    public class UserAddress
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public UserAddress()
        {
            IsActive = false; 
        }

    }
}
