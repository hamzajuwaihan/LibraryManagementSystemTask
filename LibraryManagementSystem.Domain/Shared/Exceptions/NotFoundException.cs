using System.Net;


namespace LibraryManagementSystem.Domain.Shared.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested resource is not found.
/// </summary>
/// <param name="resourceName">The name of the resource that was not found.</param>
/// <param name="key">The unique identifier of the resource.</param>
public class NotFoundException(string resourceName, object key)
    : ApiException($"{resourceName} with id '{key}' was not found.", HttpStatusCode.NotFound)
{
}