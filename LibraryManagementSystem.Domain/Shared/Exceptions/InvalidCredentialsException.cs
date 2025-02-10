using System.Net;

namespace LibraryManagementSystem.Domain.Shared.Exceptions;


/// <summary>
/// Represents an exception that is thrown when a user provides invalid credentials.
/// </summary>
public class InvalidCredentialsException : ApiException
{
    public InvalidCredentialsException()
        : base("Invalid email or password", HttpStatusCode.Unauthorized) { }
}