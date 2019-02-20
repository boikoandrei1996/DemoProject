using System.IO;
using System.Threading.Tasks;
using DemoProject.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DemoProject.WebApi.Services
{
  public class ImageService
  {
    private readonly IHostingEnvironment _environment;

    public ImageService(
      IHostingEnvironment environment)
    {
      _environment = environment;
    }

    public async Task<ServiceResult> SaveAsync(IFormFile file)
    {
      Check.NotNull(file, nameof(file));

      if (file.Length <= 0)
      {
        return ServiceResultFactory.BadRequestResult(nameof(file), "File not found.");
      }

      var relativePath = Constants.GetRelativePathToImage(file.FileName);
      var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

      if (File.Exists(fullPath))
      {
        return ServiceResultFactory.BadRequestResult(nameof(file), "File already exist.");
      }

      using (var fileStream = new FileStream(fullPath, FileMode.Create))
      {
        await file.CopyToAsync(fileStream);
      }

      return ServiceResultFactory.Success;
    }
  }
}
