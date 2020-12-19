using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Dtos;

namespace ContactsApi.Services.Interfaces
{
    public interface IAddressBookService
    {
        Task<List<ContactDto>> GetContactsAsync();
    }
}
