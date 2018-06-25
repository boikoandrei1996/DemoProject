using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Models.DiscountApiModels
{
  public class InfoObjectAddModel : IValidatableObject
  {
    [Required]
    public string Content { get; set; }

    [Required]
    public string Type { get; set; }

    public static InfoObject Map(InfoObjectAddModel model)
    {
      if (model == null)
      {
        return null;
      }

      return new InfoObject
      {
        Content = model.Content,
        Type = Enum.Parse<InfoObjectType>(model.Type, ignoreCase: true)
      };
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var model = (InfoObjectAddModel)validationContext.ObjectInstance;

      if (Enum.TryParse<InfoObjectType>(model.Type, true, out InfoObjectType temp))
      {
        yield return ValidationResult.Success;
      }
      else
      {
        var allowableValues = String.Join(", ", Enum.GetNames(typeof(InfoObjectType)));
        yield return new ValidationResult($"The {nameof(Type)} field should be in range of [{allowableValues}].", new [] { nameof(Type) });
      }
    }
  }
}
