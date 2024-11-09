// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Models;

namespace DateAppApi.IServices;

public interface IDateIdeaService
{
    Task<DateIdea> CreateDateIdeaAsync(int creatingUserId, string description);
    Task DeleteDateIdea(int userId, int dateIdeaId);
    Task<DateIdea> UpdateDateIdea(int userId, int dateIdeaId, string newDescription);
    Task<DateIdea?> GetDateIdea(int dateIdeaId);
}