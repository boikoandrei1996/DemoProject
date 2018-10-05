using System.IO;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
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

      var fullPath = Path.Combine(_webRootPath, Constants.DEFAULT_PATH_TO_IMAGE, file.FileName);
      using (var fileStream = new FileStream(fullPath, FileMode.Create))
      {
        await file.CopyToAsync(fileStream);
      }

      return ServiceResultFactory.Success;
    }
  }
}
