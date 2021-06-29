using AutoMapper;
using MeetingManager.Core.Entities;
using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingManager.Core.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<int, User>().ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src));
        }
    }
}
