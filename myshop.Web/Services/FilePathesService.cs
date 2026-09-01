using myshop.BLL.Interfaces;

namespace myshop.Web.Services
{
    public class FilePathesService : IFilePathesService
    {
        private readonly IWebHostEnvironment _environment;

        public FilePathesService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string GetSaveFilePath()
        {
            return _environment.WebRootPath;
        }
    }
}
