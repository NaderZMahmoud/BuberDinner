using ErrorOr;

namespace BuberDinner.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authintication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Authintication.InvalidCredentials",
            description: "The provided credentials are invalid.");
    }
}