using System.ComponentModel.DataAnnotations;

namespace Casino.Domain
{
    /// <summary>
    /// Playing card in deck.
    /// </summary>
    public class DeckCard
    {
        /// <summary>
        /// Card suit.
        /// </summary>
        public Suit Suit { get; set; }

        /// <summary>
        /// Card value.
        /// </summary>
        [Required]
        [MaxLength(2)]
        public string Value { get; set; }

        /// <summary>
        /// Deck identifier.
        /// </summary>
        public int DeckId { get; set; }

        /// <summary>
        /// Deck.
        /// </summary>
        public virtual Deck Deck { get; set; }

        /// <summary>
        /// Card number in deck.
        /// </summary>
        public int NumberInDeck { get; set; }
    }
}
