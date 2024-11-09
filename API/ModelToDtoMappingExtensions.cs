// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Dtos.Date;
using DateAppApi.Dtos.DateIdea;
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
    }
}