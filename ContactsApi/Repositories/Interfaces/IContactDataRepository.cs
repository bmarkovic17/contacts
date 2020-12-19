using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Models;

namespace ContactsApi.Repositories.Interfaces
{
    public interface IContactDataRepository
    {
        Task<List<ContactData>> GetContactDataAsync();
    }
}
