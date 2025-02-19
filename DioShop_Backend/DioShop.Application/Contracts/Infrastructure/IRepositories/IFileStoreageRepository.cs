using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Contracts.Infrastructure.IRepositories
{
	public interface IFileStoreageRepository
	{
		string GetFileUrl(string fileName);

		Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

		Task DeleteFileAsync(string fileName);
	}
}
