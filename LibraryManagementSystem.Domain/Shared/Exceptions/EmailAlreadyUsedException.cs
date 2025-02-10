using System.Net;


namespace LibraryManagementSystem.Domain.Shared.Exceptions;
/// <summary>
/// Represents an exception that is thrown when an email is already registered in the system.
/// </summary>
/// <param name="email">The email that is already in use.</param>
public class EmailAlreadyUsedException(string email) : ApiException($"The email '{email}' is already in use.", HttpStatusCode.BadRequest)
{
}