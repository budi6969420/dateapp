// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.DbContexts;
using DateAppApi.IServices;
using DateAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DateAppApi.Services
{
    public class DateIdeaService : IDateIdeaService
    {
        public DateIdeaService(AppDbContext context)
        {
            m_context = context;
        }

        #region IDateIdeaService Members
        public async Task<DateIdea> CreateDateIdeaAsync(int creatingUserId, string description)
        {
            if (await m_context.Users.FindAsync(creatingUserId) == null) throw new KeyNotFoundException("no such user exists");

            if (string.IsNullOrEmpty(description)) throw new InvalidOperationException("description cant be empty");
            var dateIdea = new DateIdea()
            {
                Description = description,
                CreatingUserId = creatingUserId
            };

            m_context.DateIdeas.Add(dateIdea);
            await m_context.SaveChangesAsync();
            return dateIdea;
        }

        public async Task DeleteDateIdea(int userId, int dateIdeaId)
        {
            var dateIdea = await m_context.DateIdeas
                .Include(x => x.CreatingUser)
                .Include(x => x.DatesPresentOn)
                .FirstOrDefaultAsync(x => x.Id == dateIdeaId);

            if (dateIdea == null) throw new KeyNotFoundException("date idea does not exist");
            if (dateIdea.CreatingUserId != userId) throw new UnauthorizedAccessException();
            if (dateIdea.DatesPresentOn.Any()) throw new InvalidOperationException("date idea in use in date/s");

            m_context.DateIdeas.Remove(dateIdea);
            await m_context.SaveChangesAsync();
        }

        public async Task<DateIdea> UpdateDateIdea(int userId, int dateIdeaId, string newDescription)
        {
            var dateIdea = await m_context.DateIdeas
                .Include(x => x.CreatingUser)
                .FirstOrDefaultAsync(x => x.Id == dateIdeaId);

            if (dateIdea == null) throw new KeyNotFoundException("date idea does not exist");
            if (dateIdea.CreatingUserId != userId) throw new UnauthorizedAccessException();
            if (string.IsNullOrEmpty(newDescription)) throw new InvalidOperationException("new description cant be empty");

            dateIdea.Description = newDescription;
            m_context.DateIdeas.Update(dateIdea);
            await m_context.SaveChangesAsync();
            return dateIdea;
        }

        public async Task<DateIdea?> GetDateIdea(int dateIdeaId)
        {
            return await m_context.DateIdeas
                .Include(x => x.CreatingUser)
                .Include(x => x.DatesPresentOn)
                .FirstOrDefaultAsync(x => x.Id == dateIdeaId);
        }
        #endregion

        #region private fields and constants
        private readonly AppDbContext m_context;
        #endregion
    }
}