﻿// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.DbContexts;
using DateAppApi.Helpers;
using DateAppApi.IServices;
using DateAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DateAppApi.Services
{
    public class UserService : IUserService
    {
        public UserService(AppDbContext context)
        {
            m_context = context;
        }

        #region IUserService Members
        public async Task<User> EnsureLoginOkAsync(string username, string password)
        {
            username = username.ToLower();
            var user = await m_context.Users.FirstOrDefaultAsync(x => x.Username.Equals(username));
            if (user == null) throw new KeyNotFoundException("username not linked to account");
            var hashedPass = PasswordHashingHelper.HashPassword(password);
            if (!user.HashedPassword.Equals(hashedPass)) throw new UnauthorizedAccessException();
            return user;
        }

        public async Task<User> RegisterAsync(string username, string password, char gender)
        {
            username = username.ToLower();
            var user = await m_context.Users.FirstOrDefaultAsync(x => x.Username.Equals(username));
            if (user != null) throw new BadHttpRequestException("username linked to account that already exists");

            var newUser = new User()
            {
                Username = username,
                HashedPassword = PasswordHashingHelper.HashPassword(password),
                Gender = gender
            };

            m_context.Users.Add(newUser);
            await m_context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await m_context.Users
                .Include(x => x.CreatedDateIdeas)
                .Include(x => x.CreatedDates)
                .Include(x => x.PartOfDates)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return m_context.Users.Include(x => x.CreatedDateIdeas)
                .Include(x => x.CreatedDates)
                .Include(x => x.PartOfDates);
        }

        public async Task AddNewProfilePictureAsync(int userId, byte[] imageData)
        {
            var user = await m_context.Users.Include(x => x.ProfilePicture).FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) throw new BadHttpRequestException("account does not exist");

            if (user.ProfilePicture != null) m_context.Remove(user.ProfilePicture);
            var image = new Image()
            {
                Data = imageData,
            };
            user.ProfilePicture = image;
            await m_context.SaveChangesAsync();
        }

        public async Task RemoveProfilePictureAsync(int userId)
        {
            var user = await m_context.Users.FindAsync(userId);
            if (user == null) throw new BadHttpRequestException("account does not exist");

            user.ProfilePicture = null;
            await m_context.SaveChangesAsync();
        }
        #endregion

        #region private fields and constants
        private readonly AppDbContext m_context;
        #endregion
    }
}