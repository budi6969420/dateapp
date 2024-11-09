// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
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
    }
}