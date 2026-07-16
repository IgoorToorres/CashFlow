using System.Text.RegularExpressions;
using CashFlow.Exception.Resources;
using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.Users;

public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";
    private const string ERROR_MESSAGE = "ErrorMessage";

    protected override string GetDefaultMessageTemplate(string errorCode) => $"{{{ERROR_MESSAGE}}}";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourcesErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if(password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourcesErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if(!ContainsUppercaseLetter().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourcesErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if(!ContainsLowercaseLetter().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourcesErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if(!ContainsNumber().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourcesErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if(!ContainsSpecialCharacter().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourcesErrorMessages.INVALID_PASSWORD);
            return false;
        }

        return true;
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex ContainsUppercaseLetter();
    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex ContainsLowercaseLetter();
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex ContainsNumber();
    [GeneratedRegex(@"[!/@/#/$/%/&/*/.]+")]
    private static partial Regex ContainsSpecialCharacter();
}
