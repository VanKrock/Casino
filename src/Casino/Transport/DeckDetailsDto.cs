using System.Collections.Generic;

namespace Casino.Transport
{
    /// <summary>
    /// DEck details DTO.
    /// </summary>
    public class DeckDetailsDto : DeckDto
    {
        /// <summary>
        /// Cards in deck.
        /// </summary>
        public ICollection<CardDto> Cards { get; set; }
    }
}
