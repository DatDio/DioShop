using DioShop.Application.Contracts.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Infrastructure.Repositories
{
	public class FileStorageRepository : IFileStoreageRepository
	{
		private readonly string _pathFolder;
		private const string FOLDER_NAME = "Images/ProductImages";
		public FileStorageRepository(IWebHostEnvironment webHostEnvironment) 
		{
			_pathFolder = Path.Combine(webHostEnvironment.WebRootPath, FOLDER_NAME);
		}
        public async Task DeleteFileAsync(string fileName)
        {
            // Nếu fileName là đường dẫn tương đối, lấy tên file
            var extractedFileName = Path.GetFileName(fileName);
            var filePath = Path.Combine(_pathFolder, extractedFileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string fileName)
		{
			return $"/{FOLDER_NAME}/{fileName}";
		}

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            // Lấy tên file gốc và tạo tên file mới dựa trên Guid
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            var filePath = Path.Combine(_pathFolder, fileName);

            // Lưu file vào thư mục trên server
            using var output = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(output);

          
            var relativeUrl = $"/{FOLDER_NAME}/{fileName}";
            return relativeUrl;
        }
    }
}
