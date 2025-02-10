using System.Net;
namespace LibraryManagementSystem.Domain.Shared.Exceptions;

/// <summary>
/// Represents the base class for all API-related exceptions.
/// </summary>
public abstract class ApiException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}