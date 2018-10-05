using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace DemoProject.WebApi.Services
{
  public interface IImageService
  {
    Task<ServiceResult> SaveAsync(IFormFile file);
  }
}
