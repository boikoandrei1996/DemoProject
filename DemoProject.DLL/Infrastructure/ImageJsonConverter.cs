using System;
using System.IO;
using Newtonsoft.Json;

namespace DemoProject.DLL.Infrastructure
{
  public class ImageJsonConverter : JsonConverter
  {
    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(string);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      string basePath = @"C:\Users\BoikoAndrei1996\Documents\Visual Studio 2017\Projects\DemoProject\DemoProject.DLL\StaticContent\Img";
      string path = Path.Combine(basePath, reader.Value.ToString());

      return File.ReadAllBytes(path);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }
}
