using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactsApi.Dtos;
using ContactsApi.Models;
using ContactsApi.Repositories.Interfaces;
using ContactsApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Services.Implementations
{
    public class AddressBookService : IAddressBookService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IContactDataRepository _contactDataRepository;
        private readonly IMapper _mapper;

        public AddressBookService(IContactsRepository contactsRepository, IContactDataRepository contactDataRepository, IMapper mapper)
        {
            _contactsRepository = contactsRepository;
            _contactDataRepository = contactDataRepository;
            _mapper = mapper;
        }

        public async Task<List<ContactDto>> GetContactsAsync(int? id)
        {
            List<Contact> contacts = await _contactsRepository
                .GetContacts()
                .Where(contact => !id.HasValue || contact.Id == id)
                .ToListAsync();
            Debug.Assert(contacts.Any());

            IEnumerable<int> contactIds = contacts.Select(contact => contact.Id);
            List<ContactData> contactData = await _contactDataRepository
                .GetContactData()
                .Where(contactData => contactIds.Contains(contactData.ContactId))
                .ToListAsync();
            Debug.Assert(contactData.Any());

            List<ContactDto> addressBook = new List<ContactDto>();
            
            contacts.ForEach(contact =>
            {
                addressBook.Add(_mapper.Map<ContactDto>(contact));
                
                addressBook.Last().ContactData = _mapper.Map<IEnumerable<ContactDataDto>>(
                    contactData.Where(contactData => contactData.ContactId == contact.Id));
            });

            Debug.Assert(addressBook.Any());

            return addressBook;
        }
    }
}
