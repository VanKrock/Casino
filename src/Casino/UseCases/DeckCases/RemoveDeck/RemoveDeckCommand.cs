using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Casino.UseCases.DeckCases.RemoveDeck
{
    /// <summary>
    /// Remove deck command.
    /// </summary>
    public class RemoveDeckCommand : IRequest
    {
        /// <summary>
        /// Deck name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
