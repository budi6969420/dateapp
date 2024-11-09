// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
namespace DateAppApi.Dtos.Date
{
    public class DateUpdateDto
    {
        public DateTime DateStartDate { get; set; }
        public DateTime DateEndDate { get; set; }
        public string? Description { get; set; }
    }
}