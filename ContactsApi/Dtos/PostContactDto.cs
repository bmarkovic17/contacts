using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Dtos
{
    public class PostContactDto
    {
        [Required]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string AddressNumber { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        public IEnumerable<PostContactDataDto> ContactData { get; set; }
    }
}
