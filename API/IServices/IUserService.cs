// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Models;

namespace DateAppApi.IServices;

public interface IUserService
{
    Task LoginAsync(string username, string password);
    Task<User> RegisterAsync(string username, string password);
}