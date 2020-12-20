using System.Linq;
using ContactsApi.Database;
using ContactsApi.Models;
using ContactsApi.Repositories.Interfaces;

namespace ContactsApi.Repositories.Implementations
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly AddressBookContext _addressBookContext;

        public ContactsRepository(AddressBookContext addressBookContext) =>
            _addressBookContext = addressBookContext;

        public IQueryable<Contact> GetContacts() =>
            _addressBookContext.Contacts.AsQueryable();
    }
}
