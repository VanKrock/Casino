using Casino.Abstractions;
using Casino.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.UseCases.DeckCases.RemoveDeck
{
    /// <summary>
    /// Handler for <see cref="RemoveDeckCommand"/>.
    /// </summary>
    public class RemoveDeckCommandHandler : IRequestHandler<RemoveDeckCommand, Unit>
    {
        private readonly IAppDbContext dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        public RemoveDeckCommandHandler(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(RemoveDeckCommand request, CancellationToken cancellationToken)
        {
            var deck = await dbContext.Decks.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (deck == null)
            {
                throw new NotFoundException("Deck not found");
            }
            dbContext.Decks.Remove(deck);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
