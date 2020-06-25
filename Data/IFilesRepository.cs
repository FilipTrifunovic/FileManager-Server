using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace server.Data
{
    public interface IFilesRepository
    {
          Task<decimal> AddFileForCheck(IFormFile file,string text=null);
    }
}