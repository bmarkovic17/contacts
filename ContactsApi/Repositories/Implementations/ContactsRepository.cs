using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Database;
using ContactsApi.Models;
using ContactsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Repositories.Implementations
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly AddressBookContext _contactContext;

        public ContactsRepository(AddressBookContext contactContext) =>
            _contactContext = contactContext;

        public Task<List<Contact>> GetUsersAsync() =>
            _contactContext.Contacts.ToListAsync();
    }
}
