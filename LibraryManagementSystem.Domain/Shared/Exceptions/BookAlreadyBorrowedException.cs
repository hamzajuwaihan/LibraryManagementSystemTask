using System.Net;


namespace LibraryManagementSystem.Domain.Shared.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a book is already borrowed.
/// </summary>
/// <param name="message">The error message describing the issue.</param>
public class BookAlreadyBorrowedException(string message) : ApiException(message, HttpStatusCode.BadRequest)
{
}