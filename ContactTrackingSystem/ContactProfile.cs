using AutoMapper;
using ContactTrackingSystem.Entities;
using ContactTrackingSystem.Models;

namespace ContactTrackingSystem
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactViewModel>().ReverseMap();
        }
    }
}
