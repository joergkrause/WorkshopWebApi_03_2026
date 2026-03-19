using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Workshop.DataTransferObjects.Validation;

public class IgzCustomerNumberAttribute : ValidationAttribute
{


  public string Prefix { get; set; } = "IGZ";

  public override bool IsValid(object? value)
  {
    if (value == null) return false;
    if (value.ToString()?.StartsWith(Prefix) == false) return false;
    return true;
  }

}
