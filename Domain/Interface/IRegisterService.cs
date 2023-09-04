using Identity_library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace Identity_library.Domain.Interface
{
    public interface IRegisterService
    {
        Task<Response<RegisterModel>> RegisterAsyncService(RegisterModel model);
    }
}
