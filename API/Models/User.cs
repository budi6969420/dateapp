// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
#pragma warning disable CS8618
namespace DateAppApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public char Gender { get; set; }
        public int? ProfilePictureId { get; set; }
        public Image? ProfilePicture { get; set; }
        public DateTime TimeJoined { get; set; } = DateTime.UtcNow;
        public string HashedPassword { get; set; }
        public List<DateIdea> CreatedDateIdeas { get; set; } = new List<DateIdea>();
        public List<Date> CreatedDates { get; set; } = new List<Date>();
        public List<Date> PartOfDates { get; set; } = new List<Date>();
    }
}