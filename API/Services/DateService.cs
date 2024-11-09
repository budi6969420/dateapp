﻿// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.DbContexts;
using DateAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DateAppApi.Services
{
    public class DateService
    {
        public DateService(AppDbContext context)
        {
            m_context = context;
        }

        public async Task<Date> CreateDateAsync(int creatingUserId,
            int otherUserId,
            string description,
            DateTime dateStartDate,
            DateTime dateEndDate,
            IEnumerable<int> dateIdeaIds,
            IEnumerable<byte[]> imagesData)
        {
            if (await m_context.Users.FindAsync(creatingUserId) == null) throw new KeyNotFoundException("creating user does not exist");
            if (await m_context.Users.FindAsync(otherUserId) == null) throw new KeyNotFoundException("other user does not exist");

            if (string.IsNullOrEmpty(description)) throw new InvalidOperationException("description cant be empty");
            if (dateStartDate > dateEndDate) throw new InvalidOperationException("start date can not be after end date");

            var date = new Date()
            {
                CreatingUserId = creatingUserId,
                OtherUserId = otherUserId,
                Description = description,
                DateStartDate = dateStartDate,
                DateEndDate = dateEndDate
            };

            foreach (var dateIdeaId in dateIdeaIds)
            {
                await AddDateIdeaToDateInternalAsync(date, dateIdeaId, false);
            }

            foreach (var imageData in imagesData)
            {
                await AddImageToDateInternalAsync(date, imageData, false);
            }

            m_context.Dates.Add(date);
            await m_context.SaveChangesAsync();
            return date;
        }

        public async Task DeleteDateAsync(int userId, int dateId)
        {
            var date = await m_context.Dates
                .Include(x => x.CreatingUser)
                .FirstOrDefaultAsync(x => x.Id == dateId);

            if (date == null) throw new KeyNotFoundException("date does not exist");
            if (date.CreatingUserId != userId) throw new UnauthorizedAccessException();

            m_context.Dates.Remove(date);
            await m_context.SaveChangesAsync();
        }

        public async Task<Date> UpdateDateAsync(int userId, int dateId, string newDescription, DateTime newDateStartDate, DateTime newDateEndDate)
        {
            var date = await m_context.Dates
                .Include(x => x.CreatingUser)
                .FirstOrDefaultAsync(x => x.Id == dateId);

            if (date == null) throw new KeyNotFoundException("date does not exist");
            if (date.CreatingUserId != userId) throw new UnauthorizedAccessException();
            if (string.IsNullOrEmpty(newDescription)) throw new InvalidOperationException("new description cant be empty");
            if (newDateStartDate > newDateEndDate) throw new InvalidOperationException("start date can not be after end date");

            date.Description = newDescription;
            date.DateStartDate = newDateStartDate;
            date.DateEndDate = newDateEndDate;

            m_context.Dates.Update(date);
            await m_context.SaveChangesAsync();
            return date;
        }

        public async Task AddImageToDateAsync(int dateId, int userId, byte[] imageData)
        {
            var date = await m_context.Dates.FindAsync(dateId);
            if (date == null) throw new KeyNotFoundException("date does not exist");
            if (date.CreatingUserId != userId) throw new UnauthorizedAccessException();
            await AddImageToDateInternalAsync(date, imageData);
        }

        public async Task RemoveImageToDateAsync(int dateId, int userId, int imageId)
        {
            var date = await m_context.Dates.FindAsync(dateId);
            if (date == null) throw new KeyNotFoundException("date does not exist");
            if (date.CreatingUserId != userId) throw new UnauthorizedAccessException();
            await RemoveImageToDateInternalAsync(date, imageId);
        }

        public async Task AddDateIdeaToDateAsync(int dateId, int userId, int dateIdea)
        {
            var date = await m_context.Dates.FindAsync(dateId);
            if (date == null) throw new KeyNotFoundException("date does not exist");
            if (date.CreatingUserId != userId) throw new UnauthorizedAccessException();
            await AddDateIdeaToDateInternalAsync(date, dateIdea);
        }

        public async Task RemoveDateIdeaToDateAsync(int dateId, int userId, int dateIdea)
        {
            var date = await m_context.Dates.FindAsync(dateId);
            if (date == null) throw new KeyNotFoundException("date does not exist");
            if (date.CreatingUserId != userId) throw new UnauthorizedAccessException();
            await RemoveDateIdeaToDateInternalAsync(date, dateIdea);
        }

        private async Task AddImageToDateInternalAsync(Date date, byte[] imageData, bool saveChanged = true)
        {
            var image = new Image()
            {
                Data = imageData,
            };
            date.Images.Add(image);
            if (saveChanged) await m_context.SaveChangesAsync();
        }

        private async Task AddDateIdeaToDateInternalAsync(Date date, int dateIdeaId, bool saveChanges = true)
        {
            var dateIdea = await m_context.DateIdeas.FindAsync(dateIdeaId);
            if (dateIdea == null) throw new KeyNotFoundException($"date idea with id {dateIdeaId} does not exist");
            date.DateIdeas.Add(dateIdea);
            if (saveChanges) await m_context.SaveChangesAsync();
        }

        private async Task RemoveImageToDateInternalAsync(Date date, int imageId, bool saveChanged = true)
        {
            var image = await m_context.Images.FindAsync(imageId);
            if (image != null) date.Images.Remove(image);
            if (saveChanged) await m_context.SaveChangesAsync();
        }

        private async Task RemoveDateIdeaToDateInternalAsync(Date date, int dateIdeaId, bool saveChanges = true)
        {
            var dateIdea = await m_context.DateIdeas.FindAsync(dateIdeaId);
            if (dateIdea != null) date.DateIdeas.Remove(dateIdea);
            if (saveChanges) await m_context.SaveChangesAsync();
        }

        #region private fields and constants
        private readonly AppDbContext m_context;
        #endregion
    }
}