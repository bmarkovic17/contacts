using AutoMapper;
using ContactsApi.Dtos;
using ContactsApi.Models;

namespace ContactsApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactData, ContactDataDto>();
            CreateMap<PostContactDto, Contact>();
            CreateMap<PostContactDto, ContactDto>();
            CreateMap<PostContactDataDto, ContactData>();
            CreateMap<PostContactDataDto, ContactDataDto>();
            CreateMap<DeleteContactDto, Contact>();
            CreateMap<ContactDto, DeleteContactDto>();
        }
    }
}
