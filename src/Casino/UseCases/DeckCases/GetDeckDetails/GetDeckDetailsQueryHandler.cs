using AutoMapper;
using Casino.Abstractions;
using Casino.Exceptions;
using Casino.Transport;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.UseCases.DeckCases.GetDeckDetails
{
    /// <summary>
    /// Handler for <see cref="GetDeckDetailsQuery"/>.
    /// </summary>
    public class GetDeckDetailsQueryHandler : IRequestHandler<GetDeckDetailsQuery, DeckDetailsDto>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        /// <param name="mapper">Mapper.</param>
        public GetDeckDetailsQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<DeckDetailsDto> Handle(GetDeckDetailsQuery request, CancellationToken cancellationToken)
        {
            var deck = await dbContext.Decks
                .Include(x => x.Cards)
                .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (deck == null)
            {
                throw new NotFoundException("Deck not found.");
            }
            deck.Cards = deck.Cards.OrderBy(x => x.NumberInDeck).ToList();
            return mapper.Map<DeckDetailsDto>(deck);
        }
    }
}
