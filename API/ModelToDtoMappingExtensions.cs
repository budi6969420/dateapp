// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Dtos.Date;
using DateAppApi.Dtos.DateIdea;
using DateAppApi.Dtos.User;
using DateAppApi.Models;

namespace DateAppApi
{
    public static class ModelToDtoMappingExtensions
    {
        public static DateIdeaGetDto ToDto(this DateIdea model)
        {
            return new DateIdeaGetDto()
            {
                Id = model.Id,
                CreatingUserId = model.CreatingUserId,
                DatesPresentOn = model.DatesPresentOn.Select(x => x.Id).ToArray(),
                Description = model.Description,
            };
        }

        public static DateGetDto ToDto(this Date model)
        {
            return new DateGetDto()
            {
                Id = model.Id,
                CreatingUserId = model.CreatingUserId,
                OtherUserId = model.OtherUserId,
                DateStartDate = model.DateStartDate,
                DateEndDate = model.DateEndDate,
                DateIdeaIds = model.DateIdeas.Select(x => x.Id).ToArray(),
                ImageIds = model.Images.Select(x => x.Id).ToArray(),
                Description = model.Description,
            };
        }

        public static UserGetDto ToDto(this User user)
        {
            return new UserGetDto()
            {
                Id = user.Id,
                Gender = user.Gender,
                Username = user.Username,
                ProfilePictureId = user.ProfilePictureId,
                CreatedDateIdeaIds = user.CreatedDateIdeas.Select(x => x.Id).ToArray(),
                CreatedDateIds = user.CreatedDates.Select(x => x.Id).ToArray(),
                PartOfDateIds = user.PartOfDates.Select(x => x.Id).ToArray()
            };
        }
    }
}