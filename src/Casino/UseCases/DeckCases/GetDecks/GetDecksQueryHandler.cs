using AutoMapper;
using Casino.Abstractions;
using Casino.Transport;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.UseCases.DeckCases.GetDecks
{
    /// <summary>
    /// Handler for <see cref="GetDecksQuery"/>.
    /// </summary>
    public class GetDecksQueryHandler : IRequestHandler<GetDecksQuery, DeckDto[]>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        /// <param name="mapper">Mapper.</param>
        public GetDecksQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<DeckDto[]> Handle(GetDecksQuery request, CancellationToken cancellationToken)
        {
            var decks = dbContext.Decks;
            return await mapper.ProjectTo<DeckDto>(decks).ToArrayAsync(cancellationToken);
        }
    }
}
