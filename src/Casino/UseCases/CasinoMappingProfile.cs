using AutoMapper;
using Casino.Domain;
using Casino.Transport;

namespace Casino.UseCases
{
    /// <summary>
    /// AutoMapper mapping profile.
    /// </summary>
    public class CasinoMappingProfile : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CasinoMappingProfile()
        {
            CreateMap<DeckCard, CardDto>();
            CreateMap<Deck, DeckDto>();
            CreateMap<Deck, DeckDetailsDto>();
        }
    }
}
