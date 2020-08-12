using Casino.Transport;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Casino.UseCases.DeckCases.CreateDeck
{
    /// <summary>
    /// Create new deck command.
    /// </summary>
    public class CreateDeckCommand : IRequest<IdDto>
    {
        /// <summary>
        /// Deck name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
