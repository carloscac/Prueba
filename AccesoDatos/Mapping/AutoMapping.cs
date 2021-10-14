using AutoMapper;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Modelos;

namespace AccesoDatos.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<OwnerDb, OwnerEntidad>()
                .ForMember(e => e.idOwner, s => s.MapFrom(x => x.IdOwner))
                .ForMember(e => e.Name, s => s.MapFrom(x => x.OwnerName))
                .ForMember(e => e.Address, s => s.MapFrom(x => x.OwnerAddress))
                .ForMember(e => e.LstProperties, s => s.MapFrom(x => x.PropertyDbs))
                .ReverseMap();

            CreateMap<PropertyDb, PropertyEntidad>()
                .ForMember(e=> e.lsImagenes, s=> s.MapFrom(x=> x.PropertyImageDbs))
                .ReverseMap();

            
            CreateMap<PropertyImageEntidad, PropertyImageDb>()
                .ReverseMap();
        }
    }
}
