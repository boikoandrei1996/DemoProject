using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DemoProject.WebApi.Models.ImageApiModels
{
  public class ImageUploadModel
  {
    [Required]
    public IFormFile File { get; set; }

    [Required]
    [MinLength(1)]
    public string Path { get; set; }
  }
}
