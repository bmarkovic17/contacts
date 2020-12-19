using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Models
{
    [Comment("Data about contacts")]
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOrUpdated { get; set; }
    }
}
