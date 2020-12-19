using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Models;

namespace ContactsApi.Services.Interfaces
{
    public interface IContactsService
    {
        Task<List<Contact>> GetUsersAsync();
    }
}
