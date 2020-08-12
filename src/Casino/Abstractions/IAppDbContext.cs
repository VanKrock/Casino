using Casino.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.Abstractions
{
    /// <summary>
    /// Application abstraction for unit of work.
    /// </summary>
    public interface IAppDbContext
    {
        /// <summary>
        /// Cards in deck.
        /// </summary>
        DbSet<DeckCard> DeckCards { get; }

        /// <summary>
        /// Decks.
        /// </summary>
        DbSet<Deck> Decks { get; }

        /// <summary>
        /// Save pending changes.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
