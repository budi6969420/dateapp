﻿// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
#pragma warning disable CS8618
namespace DateAppApi.Dtos.DateIdea
{
    public class DateIdeaGetDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CreatingUserId { get; set; }
        public int[] DatesPresentOn { get; set; }
    }
}