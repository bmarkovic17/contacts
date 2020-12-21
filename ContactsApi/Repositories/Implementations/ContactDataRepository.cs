using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsApi.Database;
using ContactsApi.Models;
using ContactsApi.Repositories.Interfaces;

namespace ContactsApi.Repositories.Implementations
{
    public class ContactDataRepository : IContactDataRepository
    {
        private readonly AddressBookContext _addressBookContext;

        public ContactDataRepository(AddressBookContext addressBookContext) =>
            _addressBookContext = addressBookContext;

        public IQueryable<ContactData> GetContactData() =>
            _addressBookContext.ContactData.AsQueryable();

        public async Task<IEnumerable<ContactData>> PostContactDataAsync(IEnumerable<ContactData> contactData)
        {
            _addressBookContext.ContactData.AddRange(contactData);

            _ = await _addressBookContext.SaveChangesAsync();

            return contactData;
        }
    }
}
