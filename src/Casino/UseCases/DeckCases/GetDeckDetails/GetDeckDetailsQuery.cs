using Casino.Transport;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Casino.UseCases.DeckCases.GetDeckDetails
{
    /// <summary>
    /// Get deck details query.
    /// </summary>
    public class GetDeckDetailsQuery : IRequest<DeckDetailsDto>
    {
        /// <summary>
        /// Deck name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
