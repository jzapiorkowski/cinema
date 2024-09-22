using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Core.Validators;

public class DateOnlyAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.TimeOfDay == TimeSpan.Zero;
        }
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} must be in the format yyyy-MM-dd.";
    }

}