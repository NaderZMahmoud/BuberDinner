using ErrorOr;

namespace BuberDinner.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "A user with the given email already exists.");
        public static Error UserNotFound => Error.NotFound(
            code: "User.UserNotFound",
            description: "User with this email does not exist.");
        public static Error InvalidPassword => Error.Unauthorized(
            code: "User.InvalidPassword",
            description: "Invalid password.");

    }
}