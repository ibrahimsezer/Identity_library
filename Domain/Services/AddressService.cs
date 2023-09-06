using Identity_library.Data;
using Identity_library.Domain.Interface;
using Identity_library.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityDbContext = Identity_library.Data.IdentityDbContext;

namespace Identity_library.Domain.Services
{
    public class AddressService : IAddressService
    {
        private readonly IdentityDbContext _identitydbContext;

        public AddressService(IdentityDbContext identitydbContext)
        {
            _identitydbContext = identitydbContext;
        }

        public async Task<UserAddress> CreateAddress(UserAddress model)
        {

            var existingAddress = await _identitydbContext.UserAddresses
        .FirstOrDefaultAsync(address => address.Title == model.Title);

            if (existingAddress != null)
            {
                throw new Exception($"{model.Title} already exists");
            }
            var address = new UserAddress();
            address.Title = model.Title;
            address.Address = model.Address;
            address.IsActive = model.IsActive;
            

            await _identitydbContext.AddAsync(address);
            await _identitydbContext.SaveChangesAsync();
            return address;
        }

        public async Task<UserAddress> DeleteAddress(int id)
        {
            var addressToDelete = await _identitydbContext.UserAddresses.FirstOrDefaultAsync(address => address.Id == id);

            if (addressToDelete != null)
            {
                _identitydbContext.UserAddresses.Remove(addressToDelete);
                await _identitydbContext.SaveChangesAsync();
            }

            return addressToDelete;
        }

        public async Task<IEnumerable<UserAddress>> GetAllAddress()
        {
            var address = await _identitydbContext.UserAddresses.ToListAsync();
            return address;
        }

        public async Task<UserAddress> UpdateAddress(UserAddress model)
        {
            var existingAddress = await _identitydbContext.UserAddresses
                    .FirstOrDefaultAsync(address => address.Id == model.Id);

            if (existingAddress != null)
            {
                existingAddress.Title = model.Title;
                existingAddress.Address = model.Address;
                existingAddress.IsActive = model.IsActive;

                _identitydbContext.UserAddresses.Update(existingAddress);
                await _identitydbContext.SaveChangesAsync();
            }

            return existingAddress;

        }

        public async Task<IEnumerable<UserAddress>> GetActiveAddress()
        {
            return await _identitydbContext.UserAddresses.Where(a => a.IsActive).ToListAsync();
        }
    }
}
