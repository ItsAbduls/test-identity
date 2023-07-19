using AutoMapper;
using Insig.IdentityServer.Infrastructure.Data;
using Insig.IdentityServer.Models;
using System.Security.Claims;

namespace Insig.IdentityServer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<ExternalLoginModel, AppUser>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
                .ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.Principal.FindFirst(ClaimTypes.GivenName).Value))
                .ForMember(u => u.LastName, opt => opt.MapFrom(x => x.Principal.FindFirst(ClaimTypes.Surname).Value));
        }
    }
}
