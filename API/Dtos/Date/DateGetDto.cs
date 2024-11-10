// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
#pragma warning disable CS8618
namespace DateAppApi.Dtos.Date
{
    public class DateGetDto
    {
        public int Id { get; set; }
        public int CreatingUserId { get; set; }
        public int OtherUserId { get; set; }
        public int[] DateIdeaIds { get; set; }
        public int[] ImageIds { get; set; }
        public DateTime DateStartDate { get; set; }
        public DateTime DateEndDate { get; set; }
        public string? Description { get; set; }
    }
}