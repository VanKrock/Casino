using Casino.Domain;

namespace Casino.Transport
{
    /// <summary>
    /// Playing card DTO.
    /// </summary>
    public class CardDto
    {
        /// <summary>
        /// Card value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Card suit.
        /// </summary>
        public Suit Suit { get; set; }
    }
}
