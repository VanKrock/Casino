using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Casino.Domain
{
    /// <summary>
    /// Card deck.
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Collection of cards.
        /// </summary>
        public virtual ICollection<DeckCard> Cards { get; set; }

        /// <summary>
        /// Deck name.
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
    }
}
