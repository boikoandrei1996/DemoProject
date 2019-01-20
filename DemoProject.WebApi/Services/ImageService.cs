using System.IO;
using System.Threading.Tasks;
using DemoProject.Shared;
using Microsoft.AspNetCore.Http;

namespace DemoProject.WebApi.Services
{
  public class ImageService : IImageService
  {
    private readonly string _webRootPath;

    public ImageService(string webRootPath)
    {
      _webRootPath = webRootPath;
    }

    public async Task<ServiceResult> SaveAsync(IFormFile file)
    {
      if (file == null || file.Length <= 0)
      {
        return ServiceResultFactory.BadRequestResult(nameof(file), "File not found.");
      }

      var relativePath = Constants.GetRelativePathToImage(file.FileName);
      var fullPath = Path.Combine(_webRootPath, relativePath);
      using (var fileStream = new FileStream(fullPath, FileMode.Create))
      {
        await file.CopyToAsync(fileStream);
      }

      return ServiceResultFactory.Success;
    }
  }
}
