using Casino.Abstractions;
using Casino.Domain;
using Casino.Exceptions;
using Casino.Transport;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.UseCases.DeckCases.CreateDeck
{
    /// <summary>
    /// Handler for <see cref="CreateDeckCommand"/>.
    /// </summary>
    public class CreateDeckCommandHandler : IRequestHandler<CreateDeckCommand, IdDto>
    {
        private readonly IAppDbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        public CreateDeckCommandHandler(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IdDto> Handle(CreateDeckCommand request, CancellationToken cancellationToken)
        {
            if (await dbContext.Decks.AnyAsync(x => x.Name == request.Name, cancellationToken))
            {
                throw new ConflictException("Deck already exists.");
            }

            var cards = new List<DeckCard>();
            var order = 0;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                for (int i = 2; i < 11; i++)
                {
                    cards.Add(new DeckCard { Suit = suit, Value = i.ToString(), NumberInDeck = order++ });
                }

                cards.Add(new DeckCard { Suit = suit, Value = "J", NumberInDeck = order++ });
                cards.Add(new DeckCard { Suit = suit, Value = "Q", NumberInDeck = order++ });
                cards.Add(new DeckCard { Suit = suit, Value = "K", NumberInDeck = order++ });
                cards.Add(new DeckCard { Suit = suit, Value = "A", NumberInDeck = order++ });
            }

            var deck = new Deck
            {
                Name = request.Name,
                Cards = cards
            };

            dbContext.Decks.Add(deck);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new IdDto { Id = deck.Id };
        }
    }
}
