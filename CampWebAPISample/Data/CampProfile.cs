using AutoMapper;
using CampWebAPISample.Data.Entities;
using CampWebAPISample.Models;

namespace CampWebAPISample.Data
{
    public class CampProfile: Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>()
                .ReverseMap()
                .ForMember(t => t.Location, opt => opt.Ignore());
              

            this.CreateMap<Talk, TalkModel>()
                .ReverseMap()
                .ForMember(t => t.Camp, opt => opt.Ignore())
                .ForMember(t => t.Speaker, opt => opt.Ignore());

            this.CreateMap<Speaker, SpeakerModel>()
                .ReverseMap();

        }
    }
}
