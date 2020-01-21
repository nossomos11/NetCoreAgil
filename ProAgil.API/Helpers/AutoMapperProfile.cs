using System.Linq;
using AutoMapper;
using ProAgil.API.Dtos;
using ProAgil.Domain;

namespace ProAgil.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Evento, EventoDTO>()
                .ForMember(dto => dto.Palestrantes, opt =>{
                    opt.MapFrom(e => e.PalestrantesEventos.Select(x => x.Palestrante).ToList());
                }).ReverseMap();
            CreateMap<Palestrante, PalestranteDTO>()
                .ForMember(dto => dto.Eventos, opt => {
                    opt.MapFrom(p => p.PalestrantesEventos.Select(x => x.Evento).ToList());
                }).ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
        }
    }
}