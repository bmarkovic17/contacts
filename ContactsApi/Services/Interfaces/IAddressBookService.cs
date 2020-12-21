using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Dtos;

namespace ContactsApi.Services.Interfaces
{
    public interface IAddressBookService
    {
        Task<List<ContactDto>> GetContactsAsync(int? id);
        Task<ContactDto> PostContactAsync(PostContactDto postContactDto);
        Task<int> DeleteContactAsync(DeleteContactDto deleteContactDto);
        Task<PutContactDto> PutContactAsync(PutContactDto putContactDto);
        Task<List<ContactDataDto>> GetContactDataAsync(int contactId);
        Task<PostContactForContactDataDto> PostContactDataAsync(PostContactForContactDataDto postContactForContactDataDto);
    }
}
