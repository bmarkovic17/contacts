using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Models
{
    [Comment("Data about contacts")]
    public record Contact
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [MaxLength(200)]
        public string Street { get; set; }
        [Required]
        [MaxLength(10)]
        public string AddressNumber { get; set; }
        [Required]
        [MaxLength(10)]
        public string Postcode { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Country { get; set; }
        [Required]
        [ConcurrencyCheck]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOrUpdated { get; set; }
    }
}
