// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
namespace DateAppApi.Dtos.User
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public char Gender { get; set; }
        public int? ProfilePictureId { get; set; }
        public int[] CreatedDateIdeaIds { get; set; }
        public int[] CreatedDateIds { get; set; }
        public int[] PartOfDateIds { get; set; }
    }
}