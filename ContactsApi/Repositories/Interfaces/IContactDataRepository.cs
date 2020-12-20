﻿using System.Collections.Generic;
using System.Linq;
using ContactsApi.Models;

namespace ContactsApi.Repositories.Interfaces
{
    public interface IContactDataRepository
    {
        IQueryable<ContactData> GetContactData();
    }
}
