using myshop.Web.Services.IServices;

namespace myshop.Web.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
        }

        public async Task<string> SaveImageAsync(IFormFile file, string folder)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                _environment.WebRootPath,
                "Images",
                folder);

            Directory.CreateDirectory(folderPath);

            string fullPath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(stream);

            return Path.Combine("Images", folder, fileName)
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
            if(oldImagePath is not null)
                DeleteImage(oldImagePath);

            return await SaveImageAsync(file, folderName);
        }
    }
}
