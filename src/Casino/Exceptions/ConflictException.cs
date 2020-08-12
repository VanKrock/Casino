using Microsoft.AspNetCore.Http;

namespace Casino.Exceptions
{
    /// <summary>
    /// Conflict exception. Returns 409 status code.
    /// </summary>
    public class ConflictException : DomainException
    {
        /// <inheritdoc />
        public ConflictException(string message) : base(message, StatusCodes.Status409Conflict)
        { }
    }
}
