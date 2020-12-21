using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactsApi.Dtos;
using ContactsApi.Helpers;
using ContactsApi.Models;
using ContactsApi.Repositories.Interfaces;
using ContactsApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactsApi.Services.Implementations
{
    public class AddressBookService : IAddressBookService
    {
        private readonly IAddressBookDatabase _addressBookDatabase;
        private readonly IContactsRepository _contactsRepository;
        private readonly IContactDataRepository _contactDataRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AddressBookService(
            IAddressBookDatabase addressBookDatabase,
            IContactsRepository contactsRepository,
            IContactDataRepository contactDataRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _addressBookDatabase = addressBookDatabase;
            _contactsRepository = contactsRepository;
            _contactDataRepository = contactDataRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<ContactDto>> GetContactsAsync(int? id, int? pageNumber = null, string search = null)
        {
            List<ContactDto> addressBook = new List<ContactDto>();

            if (string.IsNullOrWhiteSpace(search) || search.Length > 2)
            {

                var sqlSearch = $"%{search}%";

                PaginatedList<Contact> contacts = await PaginatedList<Contact>.CreateAsync(
                    _contactsRepository
                        .GetContacts()
                        .Where(contact =>
                            EF.Functions.ILike(contact.FirstName, sqlSearch) ||
                            EF.Functions.ILike(contact.Surname, sqlSearch) ||
                            EF.Functions.ILike(contact.Street, sqlSearch) ||
                            EF.Functions.ILike(contact.City, sqlSearch)),
                    pageNumber ?? 1,
                    _configuration.GetValue("PageSize", 10));

                IEnumerable<int> contactIds = contacts.Select(contact => contact.Id);
                List<ContactData> contactData = await _contactDataRepository
                    .GetContactData()
                    .Where(contactData => contactIds.Contains(contactData.ContactId))
                    .ToListAsync();


                contacts.ForEach(contact =>
                {
                    addressBook.Add(_mapper.Map<ContactDto>(contact));

                    addressBook.Last().ContactData = _mapper.Map<IEnumerable<ContactDataDto>>(
                        contactData.Where(contactData => contactData.ContactId == contact.Id));
                });
            }

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
            ContactDto contact = (await GetContactsAsync(contactId)).Single();

            return _mapper.Map<List<ContactDataDto>>(contact.ContactData);
        }

        public async Task<PostContactForContactDataDto> PostContactDataAsync(PostContactForContactDataDto postContactForContactDataDto)
        {
            _ = await _addressBookDatabase.BeginTransactionAsync();

            Contact contact = _contactsRepository
                .GetContacts()
                .AsNoTracking()
                .Single(contact => contact.Id == postContactForContactDataDto.ContactId);

            IEnumerable<ContactData> contactData = await _contactDataRepository
                .GetContactData()
                .AsNoTracking()
                .Where(contactData => contactData.ContactId == contact.Id)
                .ToListAsync();

            IEnumerable<ContactData> contactDataWithId = _mapper.Map<IEnumerable<ContactData>>(
                postContactForContactDataDto.ContactData
                    .Where(contactData => contactData.Id.HasValue));

            Queue<ContactData> contactDataToDelete = new Queue<ContactData>();

            foreach (ContactData c1 in contactData)
            {
                var exists = false;

                foreach (ContactData c2 in contactDataWithId)
                {
                    if (c2.Id == c1.Id)
                    {
                        exists = true;

                        break;
                    }
                }

                if (!exists)
                    contactDataToDelete.Enqueue(c1);
            }

            // Delete
            if (contactDataToDelete.Any())
                _ = await _contactDataRepository.DeleteContactAsync(contactDataToDelete);

            // Update
            if (contactDataWithId.Any())
            {
                _ = await _contactDataRepository.DeleteContactAsync(contactDataWithId.Except(contactDataToDelete));

                _ = await _contactDataRepository.PostContactDataAsync(contactDataWithId
                    .Select(addContactData => new ContactData
                    {
                        ContactId = contact.Id,
                        ContactDataType = addContactData.ContactDataType,
                        ContactDataValue = addContactData.ContactDataValue,
                    }));
            }

            IEnumerable<ContactData> contactDataWithoutId = _mapper.Map<IEnumerable<ContactData>>(
                postContactForContactDataDto.ContactData
                    .Where(contactData => contactData.Id is null));

            // Create
            if (contactDataWithoutId.Any())
            {
                foreach (var addContactData in contactDataWithoutId)
                {
                    addContactData.ContactId = contact.Id;
                }
                
                _ = await _contactDataRepository.PostContactDataAsync(contactDataWithoutId);
            }

            await _addressBookDatabase.CommitAsync();

            return new PostContactForContactDataDto
            {
                ContactId = postContactForContactDataDto.ContactId,
                ContactData = _mapper.Map<IEnumerable<PostContactDataForContactDto>>(await GetContactDataAsync(postContactForContactDataDto.ContactId))
            };
        }
    }
}
