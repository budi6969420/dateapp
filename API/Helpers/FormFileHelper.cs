// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
namespace DateAppApi.Helpers
{
    public static class FormFileHelper
    {
        public static async Task<byte[]> ToByteArrayAsync(IFormFile file)
        {
            if (file.Length == 0) throw new ArgumentException("file provided empty");
            if (!IsImage(file)) throw new ArgumentException("file provided was not an image");
            if (!IsSizeOk(file)) throw new ArgumentException("file provided was larger than supported");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        private static bool IsImage(IFormFile file)
        {
            var imageMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/tiff", "image/webp" };

            return imageMimeTypes.Contains(file.ContentType.ToLower());
        }

        private static bool IsSizeOk(IFormFile file) => file.Length <= c_maxSizeInBytes;

        #region private fields and constants
        private const long c_maxSizeInBytes = 10 * 1024 * 1024; // 10 MB
        #endregion
    }
}