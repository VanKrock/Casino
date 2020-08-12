using Casino.Transport;
using MediatR;

namespace Casino.UseCases.DeckCases.GetDecks
{
    /// <summary>
    /// Get decks query.
    /// </summary>
    public class GetDecksQuery : IRequest<DeckDto[]>
    {
    }
}
