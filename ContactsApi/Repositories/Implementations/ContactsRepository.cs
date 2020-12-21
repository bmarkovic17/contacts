using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Contact> PostContactAsync(Contact contact)
        {
            _addressBookContext.Contacts.Add(contact);

            _ = await _addressBookContext.SaveChangesAsync();

            return contact;
        }

        public Task<int> DeleteContactAsync(Contact contact)
        {
            _addressBookContext.Contacts.Remove(contact);

            var rowCount = _addressBookContext.SaveChangesAsync();

            return rowCount;
        }
    }
}
