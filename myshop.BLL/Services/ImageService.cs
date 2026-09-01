using myshop.BLL.Interfaces;

namespace myshop.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IFilePathesService _filePathesService;
        public ImageService(IFilePathesService filePathesService)
        {
            _filePathesService = filePathesService;
        }
        public async Task<string> SaveAsync(
            string fileName,
            Stream content,
            string folder)
        {
            var relativeFolder = Path.Combine("images", folder);

            var folderPath = Path.Combine(
                _filePathesService.GetSaveFilePath(),
                relativeFolder);

            Directory.CreateDirectory(folderPath);

            var extension = Path.GetExtension(fileName);

            var newFileName = Guid.NewGuid() + extension;

            var fullPath = Path.Combine(folderPath, newFileName);

            using var stream = new FileStream(
                fullPath,
                FileMode.Create);

            await content.CopyToAsync(stream);

            return Path.Combine(relativeFolder, newFileName)
                .Replace("\\", "/");
        }

        public void Delete(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            var fullPath = Path.Combine(
                _filePathesService.GetSaveFilePath(),
                imagePath);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        public async Task<string> ReplaceAsync(
            string fileName,
            Stream content,
            string? oldImagePath,
            string folder)
        {
            if (!string.IsNullOrWhiteSpace(oldImagePath))
                Delete(oldImagePath);
            // we should put transaction to ensure the new image is saved before deleting
            return await SaveAsync(
                fileName,
                content,
                folder);
        }
    }
}
