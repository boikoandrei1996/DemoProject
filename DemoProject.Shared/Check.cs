﻿using System;

namespace DemoProject.Shared
{
  public static class Check
  {
    public static void NotNull<T>(T value, string paramName)
      where T : class
    {
      if (value == null)
      {
        throw new ArgumentNullException(paramName);
      }
    }

    public static void NotNullOrEmpty(string value, string paramName)
    {
      if (string.IsNullOrEmpty(value))
      {
        throw new ArgumentException($"{paramName} is null or empty.", paramName);
      }
    }

    public static void NotNullOrWhiteSpace(string value, string paramName)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        throw new ArgumentException($"{paramName} is null, empty or whitespace.", paramName);
      }
    }

    public static void Positive(decimal value, string paramName)
    {
      if (value <= 0)
      {
        throw new ArgumentException($"{paramName} should be positive.", paramName);
      }
    }

    public static void Positive(int value, string paramName)
    {
      if (value <= 0)
      {
        throw new ArgumentException($"{paramName} should be positive.", paramName);
      }
    }

    public static void PositiveOr0(int value, string paramName)
    {
      if (value < 0)
      {
        throw new ArgumentException($"{paramName} should be positive or 0.", paramName);
      }
    }
  }
}
