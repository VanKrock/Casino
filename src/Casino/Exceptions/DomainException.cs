using Microsoft.AspNetCore.Http;
using System;

namespace Casino.Exceptions
{
    /// <summary>
    /// Domain exception. Returns 400 status cote by default.
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public DomainException(string message) : this(message, StatusCodes.Status400BadRequest)
        {
        }

        /// <inheritdoc cref="DomainException(string)"/>
        /// <param name="statusCode">Http status code.</param>
        public DomainException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Http status code.
        /// </summary>
        public int StatusCode { get; }
    }
}
