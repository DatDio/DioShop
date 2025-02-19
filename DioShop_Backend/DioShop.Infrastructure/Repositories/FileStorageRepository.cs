﻿using DioShop.Application.Contracts.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
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
			var filePath = Path.Combine(_pathFolder, fileName);
			if (File.Exists(filePath))
			{
				await Task.Run(() => File.Delete(filePath));
			}	
		}

		public string GetFileUrl(string fileName)
		{
			return $"/{FOLDER_NAME}/{fileName}";
		}

		public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
		{
			var filePath = Path.Combine(_pathFolder, fileName);
			using var output = new FileStream(filePath, FileMode.Create);
			await mediaBinaryStream.CopyToAsync(output);
		}
	}
}
