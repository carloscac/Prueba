using ApiInmobiliaria.Models;
using AutoMapper;
using Entidades.Entidades;

namespace ApiInmobiliaria.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<PropertyEntidad, PropertyModel>()
                .ReverseMap();


        }
    }
}
