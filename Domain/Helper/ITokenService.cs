﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity_library.Domain.Helper
{
    public interface ITokenService
    {
        string GenerateToken(string userName, Claim[] claims);

    }
}
