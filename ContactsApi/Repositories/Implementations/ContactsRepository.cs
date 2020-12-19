﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Database;
using ContactsApi.Models;
using ContactsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Repositories.Implementations
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly AddressBookContext _addressBookContext;

        public ContactsRepository(AddressBookContext addressBookContext) =>
            _addressBookContext = addressBookContext;

        public Task<List<Contact>> GetContactsAsync() =>
            _addressBookContext.Contacts.ToListAsync();
    }
}