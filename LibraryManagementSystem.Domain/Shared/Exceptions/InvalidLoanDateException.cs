using System.Net;

namespace LibraryManagementSystem.Domain.Shared.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an invalid loan date is provided.
/// </summary>
/// <param name="message">The error message describing the issue.</param>
public class InvalidLoanDateException(string message) : ApiException(message, HttpStatusCode.BadRequest)
{
}