using System.Linq;
using ContactsApi.Models;

namespace ContactsApi.Repositories.Interfaces
{
    public interface IContactsRepository
    {
        IQueryable<Contact> GetContacts();
    }
}
