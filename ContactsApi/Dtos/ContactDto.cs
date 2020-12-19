using System;
using System.Collections.Generic;
using ContactsApi.Models;

namespace ContactsApi.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Street { get; set; }
        public string AddressNumber { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CreatedOrUpdated { get; set; }

        public IEnumerable<ContactDataDto> ContactData { get; set; }
    }
}
