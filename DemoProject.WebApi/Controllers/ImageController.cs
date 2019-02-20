using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;
using DemoProject.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Controllers
{
  [Route("api/[controller]")]
  [HandleServiceResult]
  [ValidateModelState]
  public class ImageController : Controller
  {
    private readonly ImageService _imageService;

    public ImageController(
      ImageService imageService)
    {
      _imageService = imageService;
    }

    // GET api/image/all
    [HttpGet("all")]
    public IEnumerable<string> GetAll()
    {
      return _imageService.GetImages();
    }

    // POST api/image/upload
    [HttpPost("upload")]
    public Task<ServiceResult> Upload(IFormFile file, string path)
    {
      return _imageService.SaveAsync(file, path);
    }

    // DELETE api/image
    [HttpDelete]
    public ServiceResult Delete([FromQuery]string path)
    {
      return _imageService.Delete(path);
    }
  }
}