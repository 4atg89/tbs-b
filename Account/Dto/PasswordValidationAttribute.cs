using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Account.Dto;

public class PasswordValidationAttribute : ValidationAttribute
{

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var password = value as string;

        if (string.IsNullOrWhiteSpace(password))
        {
            return new ValidationResult("Password should not be empty.");
        }

        if (password.Length < 8)
        {
            return new ValidationResult("Password shoulb be more then 8 length");
        }

        if (!password.Any(char.IsDigit))
        {
            return new ValidationResult("Password should contain at least 1 digit");
        }

        if (!password.Any(char.IsUpper))
        {
            return new ValidationResult("Password should contain Upper and lower case.");
        }

        if (!password.Any(char.IsLower))
        {
            return new ValidationResult("Password should contain Upper and lower case.");
        }

        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return new ValidationResult("Password should contain special symbol.");
        }

        return ValidationResult.Success;
    }
}