// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
namespace DateAppApi.Dtos.Date
{
    public class DateAddDto
    {
        public int Id { get; set; }
        public int CreatingUserId { get; set; }
        public int OtherUserId { get; set; }
        public int[] DateIdeaIds { get; set; }
        public IEnumerable<byte[]> ImageDatas { get; set; }
        public DateTime DateStartDate { get; set; }
        public DateTime DateEndDate { get; set; }
        public string? Description { get; set; }
    }
}