using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Casino.UseCases.DeckCases.ShuffleDeck
{
    /// <summary>
    /// Shuffle deck command.
    /// </summary>
    public class ShuffleDeckCommand : IRequest
    {
        /// <summary>
        /// Deck name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
