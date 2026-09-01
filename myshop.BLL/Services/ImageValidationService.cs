using myshop.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    public class ImageValidationService : IImageValidationService
    {
        private static readonly string[] AllowedExtensions =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };
        private const long MaxSize = 2 * 1024 * 1024;

        public bool IsValid(string extension, long size)
        {
            if (string.IsNullOrWhiteSpace(extension))
                return false;

            extension = extension.ToLowerInvariant();

            return AllowedExtensions.Contains(extension)
                   && size <= MaxSize;
        }
    }
}
