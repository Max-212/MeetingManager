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
            CreateMap<Meeting, MeetingModel>();
            CreateMap<MeetingModel, Meeting>();
        }
    }
}
