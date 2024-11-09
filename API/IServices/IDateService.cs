// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Models;

namespace DateAppApi.IServices;

public interface IDateService
{
    Task<Date> CreateDateAsync(int creatingUserId,
        int otherUserId,
        string description,
        DateTime dateStartDate,
        DateTime dateEndDate,
        IEnumerable<int> dateIdeaIds,
        IEnumerable<byte[]> imagesData);

    Task DeleteDateAsync(int userId, int dateId);
    Task<Date> UpdateDateAsync(int userId, int dateId, string newDescription, DateTime newDateStartDate, DateTime newDateEndDate);
    Task AddImageToDateAsync(int dateId, int userId, byte[] imageData);
    Task RemoveImageFromDateAsync(int dateId, int userId, int imageId);
    Task AddDateIdeaToDateAsync(int dateId, int userId, int dateIdea);
    Task RemoveDateIdeaFromDateAsync(int dateId, int userId, int dateIdea);
}