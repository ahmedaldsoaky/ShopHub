namespace myshop.BLL.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveAsync(
            string fileName,
            Stream content,
            string folder);

        void Delete(string? imagePath);

        Task<string> ReplaceAsync(
            string fileName,
            Stream content,
            string? oldImagePath,
            string folder);
    }
}
