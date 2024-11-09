// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
namespace DateAppApi.Models
{
    public class DateIdea
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int CreatingUserId { get; set; }
        public User CreatingUser { get; set; }
        public List<Date> DatesPresentOn { get; set; } = new List<Date>();
    }
}