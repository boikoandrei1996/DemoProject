﻿using System.ComponentModel.DataAnnotations;

namespace DemoProject.WebApi.Models.UserApiModels
{
  public sealed class UserAuthModel
  {
    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Username { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Password { get; set; }
  }
}
