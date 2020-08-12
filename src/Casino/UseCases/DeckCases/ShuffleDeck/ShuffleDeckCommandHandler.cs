using Casino.Abstractions;
using Casino.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.UseCases.DeckCases.ShuffleDeck
{
    /// <summary>
    /// Handler for <see cref="ShuffleDeckCommand"/>.
    /// </summary>
    public class ShuffleDeckCommandHandler : IRequestHandler<ShuffleDeckCommand, Unit>
    {
        private readonly IAppDbContext dbContext;
        private readonly IShuffleDeckService shuffleService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        /// <param name="shuffleService">Shuffle service.</param>
        public ShuffleDeckCommandHandler(IAppDbContext dbContext, IShuffleDeckService shuffleService)
        {
            this.dbContext = dbContext;
            this.shuffleService = shuffleService;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(ShuffleDeckCommand request, CancellationToken cancellationToken)
        {
            var deck = await dbContext.Decks
                .Include(x => x.Cards)
                .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (deck == null)
            {
                throw new NotFoundException("Deck not found.");
            }

            await shuffleService.ShuffleDeckAsync(deck, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
