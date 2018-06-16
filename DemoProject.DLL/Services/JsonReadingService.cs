using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DemoProject.DLL.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DemoProject.DLL.Services
{
  public class JsonReadingService : IFileReadingService
  {
    private readonly string _dirPath;
    private readonly ILogger _logger;

    public JsonReadingService(string dirPath) : this(dirPath, null) { }

    public JsonReadingService(string dirPath, ILogger logger)
    {
      _dirPath = dirPath;
      _logger = logger;
    }

    public async Task<T> LoadAsync<T>(string fileName)
    {
      try
      {
        string path = Path.Combine(_dirPath, fileName);

        string json = await File.ReadAllTextAsync(path, Encoding.UTF8);

        return JsonConvert.DeserializeObject<T>(json);
      }
      catch (Exception ex)
      {
        if (_logger != null && _logger.IsEnabled(LogLevel.Error))
        {
          _logger.LogError(ex, $"{nameof(LoadAsync)} exception: type: \"{ex.GetType()}\" message: \"{ex.Message}\"");
        }

        return default(T);
      }
    }
  }
}
