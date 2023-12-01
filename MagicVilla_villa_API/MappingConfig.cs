using AutoMapper;
using MagicVilla_villa_API.Models;
using MagicVilla_villa_API.Models.DTO;

namespace MagicVilla_villa_API
{
    public class MappingConfig : Profile
    {
        // First install Automappet packges 
        //autoMapper
        //AutoMapper extensions for ASP.NET Core
        // after add below line in Program.cs
        //builder.Services.AddAutoMapper(typeof(MappingConfig));
        public MappingConfig()
        {
            CreateMap<Villa,VillaDto>().ReverseMap();
        }
    }
}
