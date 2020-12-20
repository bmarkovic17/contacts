using System.Linq;
using System.Threading.Tasks;
using ContactsApi.Models;

namespace ContactsApi.Repositories.Interfaces
{
    public interface IContactsRepository
    {
        IQueryable<Contact> GetContacts();
        Task<Contact> PostContactAsync(Contact contact);
    }
}
