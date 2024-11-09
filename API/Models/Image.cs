// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
namespace DateAppApi.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public User? ProfilePictureOfUser { get; set; }
        public Date? PictureOfDate { get; set; }
    }
}