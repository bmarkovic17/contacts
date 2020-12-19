using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Models;
using ContactsApi.Repositories.Interfaces;
using ContactsApi.Services.Interfaces;

namespace ContactsApi.Services.Implementations
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsService(IContactsRepository contactsRepository) =>
            _contactsRepository = contactsRepository;

        public Task<List<Contact>> GetUsersAsync() =>
            _contactsRepository.GetUsersAsync();
    }
}
