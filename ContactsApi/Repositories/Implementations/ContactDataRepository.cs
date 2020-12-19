using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Database;
using ContactsApi.Models;
using ContactsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Repositories.Implementations
{
    public class ContactDataRepository : IContactDataRepository
    {
        private readonly AddressBookContext _addressBookContext;

        public ContactDataRepository(AddressBookContext addressBookContext) =>
            _addressBookContext = addressBookContext;

        public Task<List<ContactData>> GetContactDataAsync() =>
            _addressBookContext.ContactData.ToListAsync();
    }
}
