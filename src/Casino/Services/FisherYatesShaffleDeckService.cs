using Casino.Abstractions;
using Casino.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.Services
{
    /// <summary>
    /// Shuffle service used <see cref="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle">Fisher-Yates algorithm</see>.
    /// </summary>
    public class FisherYatesShaffleDeckService : IShuffleDeckService
    {
        /// <inheritdoc />
        public Task ShuffleDeckAsync(Deck deck, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var random = new Random(); // It's pseudo random generator, I hope is's enough for this task.
                var cards = deck.Cards.ToArray();
                for (var i = 0; i < cards.Length; i++)
                {
                    var index = random.Next(0, i + 1);
                    var temp = cards[i].NumberInDeck;
                    cards[i].NumberInDeck = cards[index].NumberInDeck;
                    cards[index].NumberInDeck = temp;
                }
                deck.Cards = cards;
            }, cancellationToken);
        }
    }
}
