using Casino.Transport;
using Casino.UseCases.DeckCases.CreateDeck;
using Casino.UseCases.DeckCases.GetDeckDetails;
using Casino.UseCases.DeckCases.GetDecks;
using Casino.UseCases.DeckCases.RemoveDeck;
using Casino.UseCases.DeckCases.ShuffleDeck;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Casino.Controllers
{
    [ApiController]
    [Route("api/deck")]
    public class DeckController : Controller
    {
        private readonly IMediator mediator;

        public DeckController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<DeckDto[]> GetDecks(CancellationToken cancellationToken)
        {
            return mediator.Send(new GetDecksQuery(), cancellationToken);
        }

        [HttpGet("details")]
        public Task<DeckDetailsDto> GetDeckDetails([FromQuery] GetDeckDetailsQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpPost]
        public Task<IdDto> CreateDeck(CreateDeckCommand command, CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("shuffle")]
        public Task ShuffleDeck(ShuffleDeckCommand command, CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpDelete]
        public Task RemoveDeck([FromQuery] RemoveDeckCommand command, CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }
    }
}
