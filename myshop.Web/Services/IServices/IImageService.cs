namespace myshop.Web.Services.IServices
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file, string folder);
        void DeleteImage(string imagePath);
        Task<string> ReplaceImageAsync(
        IFormFile? file,
        string? oldImagePath,
        string folderName);

    }
}
