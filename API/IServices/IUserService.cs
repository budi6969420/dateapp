// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Models;

namespace DateAppApi.IServices;

public interface IUserService
{
    Task<User> EnsureLoginOkAsync(string username, string password);
    Task<User?> GetUserAsync(int id);
    IEnumerable<User> GetAllUsers();
    Task<User> RegisterAsync(string username, string password, char gender);
    Task AddNewProfilePictureAsync(int userId, byte[] imageData);
    Task RemoveProfilePictureAsync(int userId);
}