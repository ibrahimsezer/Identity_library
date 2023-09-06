﻿using Identity_library.Domain.Models.Entities;

namespace Identity_library.Domain.Interface
{
    public interface IAddressService
    {
        Task<IEnumerable<UserAddress>> GetAllAddress();
        Task<UserAddress> CreateAddress(UserAddress model);
        Task<UserAddress> UpdateAddress(UserAddress model);
        Task<UserAddress> DeleteAddress(string Title);
    }
}
