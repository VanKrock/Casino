using Microsoft.AspNetCore.Http;

namespace Casino.Exceptions
{
    /// <summary>
    /// Not found exception. Returns 404 status code.
    /// </summary>
    public class NotFoundException : DomainException
    {
        /// <inheritdoc />
        public NotFoundException(string message) : base(message, StatusCodes.Status404NotFound)
        { }
    }
}
