using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DioShop.Application.Contracts.Infrastructure.IRepositories
{
	public interface IFileStoreageRepository
	{
		string GetFileUrl(string fileName);

		Task<string> SaveFileAsync(IFormFile file );

		Task DeleteFileAsync(string fileName);
	}
}
