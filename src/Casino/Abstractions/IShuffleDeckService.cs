using Casino.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.Abstractions
{
    /// <summary>
    /// Deck shuffle service.
    /// </summary>
    public interface IShuffleDeckService
    {
        /// <summary>
        /// Shuffle deck.
        /// </summary>
        /// <param name="deck">Source deck.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Shuffled deck.</returns>
        Task ShuffleDeckAsync(Deck deck, CancellationToken cancellationToken);
    }
}
