using AutoMapper;
using DvdStore.Dtos;
using DvdStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DvdStore.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(cdto => cdto.Id, opt => opt.Ignore());
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());

            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<MembershipTypeDto, MembershipType>().ForMember(mtdto => mtdto.Id, opt => opt.Ignore());
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<GenreDto, Genre>().ForMember(gdto => gdto.Id, opt => opt.Ignore());
        }
    }
}