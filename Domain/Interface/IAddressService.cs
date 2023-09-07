using Identity_library.Domain.Models.Entities;

namespace Identity_library.Domain.Interface
{
    public interface IAddressService
    {
        Task<UserAddress> GetActiveAddress();
        Task<IEnumerable<UserAddress>> GetAllAddress();
        Task<UserAddress> CreateAddress(UserAddress model);
        Task<UserAddress> UpdateAddress(UserAddress model);
        Task<UserAddress> DeleteAddress(int id);
    }
}
