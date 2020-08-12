using Casino.Abstractions;
using Casino.Domain;
using Casino.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.Services
{
    /// <summary>
    /// Shuffle service used custom algorithm.
    /// </summary>
    public class CustomShuffleDeckService : IShuffleDeckService
    {
        private readonly IOptions<CustomShuffleOptions> options;

        public CustomShuffleDeckService(IOptions<CustomShuffleOptions> options)
        {
            this.options = options;
        }

        /// <inheritdoc />
        public Task ShuffleDeckAsync(Deck deck, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var random = new Random();
                var cardNumbers = deck.Cards.Select(x => x.NumberInDeck).ToList();
                for (var swapNumber = 0; swapNumber < options.Value.SwapsCount; swapNumber++)
                {
                    var cardsToSwapCount = random.Next(1, cardNumbers.Count);
                    var temp = new List<int>(cardNumbers.Skip(cardsToSwapCount));
                    temp.AddRange(cardNumbers.Take(cardsToSwapCount));
                    cardNumbers = temp;
                }

                int i = 0;
                foreach (var card in deck.Cards)
                {
                    card.NumberInDeck = cardNumbers[i];
                    i++;
                }
            }, cancellationToken);
        }
    }
}
