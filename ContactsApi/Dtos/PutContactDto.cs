using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Dtos
{
    public class PutContactDto
    {
        [Required]
        public int Id { get; set; }
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
        [Required]
        public DateTime CreatedOrUpdated { get; set; }
    }
}
