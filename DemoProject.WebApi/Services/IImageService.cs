using System.Threading.Tasks;
using DemoProject.Shared;
using Microsoft.AspNetCore.Http;

namespace DemoProject.WebApi.Services
{
  public interface IImageService
  {
    Task<ServiceResult> SaveAsync(IFormFile file);
  }
}
