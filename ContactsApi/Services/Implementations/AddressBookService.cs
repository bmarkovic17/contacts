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
        private readonly IAddressBookDatabase _addressBookDatabase;
        private readonly IContactsRepository _contactsRepository;
        private readonly IContactDataRepository _contactDataRepository;
        private readonly IMapper _mapper;

        public AddressBookService(
            IAddressBookDatabase addressBookDatabase,
            IContactsRepository contactsRepository,
            IContactDataRepository contactDataRepository,
            IMapper mapper)
        {
            _addressBookDatabase = addressBookDatabase;
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

            IEnumerable<int> contactIds = contacts.Select(contact => contact.Id);
            List<ContactData> contactData = await _contactDataRepository
                .GetContactData()
                .Where(contactData => contactIds.Contains(contactData.ContactId))
                .ToListAsync();

            List<ContactDto> addressBook = new List<ContactDto>();
            
            contacts.ForEach(contact =>
            {
                addressBook.Add(_mapper.Map<ContactDto>(contact));
                
                addressBook.Last().ContactData = _mapper.Map<IEnumerable<ContactDataDto>>(
                    contactData.Where(contactData => contactData.ContactId == contact.Id));
            });

            return addressBook;
        }

        public async Task<ContactDto> PostContactAsync(PostContactDto postContactDto)
        {
            _ = await _addressBookDatabase.BeginTransactionAsync();

            var contact = _mapper.Map<Contact>(postContactDto);
            contact = await _contactsRepository.PostContactAsync(contact);
            
            var contactData = _mapper
                .Map<IEnumerable<ContactData>>(postContactDto.ContactData)
                .Select(contactData => new ContactData
                {
                    ContactId = contact.Id,
                    ContactDataStatus = "Y",
                    ContactDataType = contactData.ContactDataType,
                    ContactDataValue = contactData.ContactDataValue
                });
            contactData = await _contactDataRepository.PostContactDataAsync(contactData);

            ContactDto contactDto = _mapper.Map<ContactDto>(contact);
            contactDto.ContactData = _mapper.Map<IEnumerable<ContactDataDto>>(contactData);

            await _addressBookDatabase.CommitAsync();

            return contactDto;
        }
    }
}
