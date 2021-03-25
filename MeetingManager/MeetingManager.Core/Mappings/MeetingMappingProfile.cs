using AutoMapper;
using MeetingManager.Core.Entities;
using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingManager.Core.Mappings
{
    public class MeetingMappingProfile : Profile
    {
        public MeetingMappingProfile()
        {
            CreateMap<Page<User>, Page<UserModel>>();
            CreateMap<Meeting, MeetingModel>();
            CreateMap<MeetingRequestModel, Meeting>();
        }
    }
}
