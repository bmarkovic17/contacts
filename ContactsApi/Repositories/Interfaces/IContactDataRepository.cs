using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsApi.Models;

namespace ContactsApi.Repositories.Interfaces
{
    public interface IContactDataRepository
    {
        IQueryable<ContactData> GetContactData();
        Task<IEnumerable<ContactData>> PostContactDataAsync(IEnumerable<ContactData> contactData);
        Task<int> DeleteContactAsync(IEnumerable<ContactData> contactData);
    }
}
