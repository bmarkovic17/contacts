using System.Collections.Generic;
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

        public Task<int> DeleteContactAsync(DeleteContactDto deleteContactDto) =>
            _contactsRepository.DeleteContactAsync(_mapper.Map<Contact>(deleteContactDto));

        public async Task<PutContactDto> PutContactAsync(PutContactDto putContactDto)
        {
            Contact contact = await _contactsRepository
                .GetContacts()
                .SingleAsync(contact => contact.Id == putContactDto.Id);

            contact.FirstName = putContactDto.FirstName;
            contact.Surname = putContactDto.Surname;
            contact.DateOfBirth = putContactDto.DateOfBirth;
            contact.Street = putContactDto.Street;
            contact.AddressNumber = putContactDto.AddressNumber;
            contact.Postcode = putContactDto.Postcode;
            contact.City = putContactDto.City;
            contact.Country = putContactDto.Country;
            contact.CreatedOrUpdated = putContactDto.CreatedOrUpdated;

            await _addressBookDatabase.SaveChangesAsync();

            return _mapper
                .Map<PutContactDto>(await _contactsRepository
                    .GetContacts()
                    .SingleAsync(contact => contact.Id == putContactDto.Id));
        }

        public async Task<List<ContactDataDto>> GetContactDataAsync(int contactId)
        {
            List<ContactData> contactData = await _contactDataRepository
                .GetContactData()
                .Where(singleContactData => singleContactData.ContactId == contactId)
                .ToListAsync();

            return _mapper.Map<List<ContactDataDto>>(contactData);
        }
    }
}
