// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
namespace DateAppApi.Models
{
    public class Date
    {
        public int Id { get; set; }
        public int CreatingUserId { get; set; }
        public User CreatingUser { get; set; }
        public int OtherUserId { get; set; }
        public User OtherUser { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<DateIdea> DateIdeas { get; set; } = new List<DateIdea>();
        public DateTime DateStartDate { get; set; }
        public DateTime DateEndDate { get; set; }
        public string? Description { get; set; }
    }
}