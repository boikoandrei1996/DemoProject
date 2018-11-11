using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.WebApi.Attributes;
using DemoProject.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [ApiExplorerSettings(IgnoreApi = true)]
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public class ImageController : Controller
  {
    private readonly IImageService _imageService;

    public ImageController(
      IImageService imageService)
    {
      _imageService = imageService;
    }

    // TODO: should be tested
    // POST api/image/upload
    [HttpPost("upload")]
    public Task<ServiceResult> Upload(IFormFile file)
    {
      return _imageService.SaveAsync(file);
    }
  }
}