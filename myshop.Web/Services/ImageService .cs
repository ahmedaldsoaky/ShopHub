using myshop.Web.Services.IServices;

namespace myshop.Web.Services
{
    public class ImageService : IImageService
    {
        private static readonly string[] AllowedExtensions =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

        private const long MaxSize = 2 * 1024 * 1024;
        
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
        }

        public async Task<string> SaveImageAsync(IFormFile file, string folder)
        {
            var relativeFolder = Path.Combine("images", folder);
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!AllowedExtensions.Contains(extension))
                throw new InvalidOperationException("Only jpg, jpeg, png and webp images are allowed.");

            if (file.Length > MaxSize)
                throw new InvalidOperationException("Maximum image size is 2 MB.");

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                _environment.WebRootPath,
                relativeFolder);

            Directory.CreateDirectory(folderPath);

            string fullPath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(stream);

            return Path.Combine(relativeFolder, fileName)
                .Replace("\\", "/");
        }
        public void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            string fullPath = Path.Combine(_environment.WebRootPath, imagePath);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        public async Task<string> ReplaceImageAsync(IFormFile file, string? oldImagePath, string folderName)
        {
            if (!string.IsNullOrWhiteSpace(oldImagePath))
                DeleteImage(oldImagePath);

            return await SaveImageAsync(file, folderName);
        }
    }
}
