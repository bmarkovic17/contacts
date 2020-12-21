using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Models
{
    [Comment("Additional information about contacts")]
    public record ContactData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        [Comment("Code which designates type of contact data (e.g. phone, mail, ...)")]
        public string ContactDataType { get; set; }
        [Required]
        [MaxLength(50)]
        [Comment("Concrete value of contact data (e.g. contact's phone number)")]
        public string ContactDataValue { get; set; }
        [Required]
        [ConcurrencyCheck]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOrUpdated { get; set; }

        [Required]
        public int ContactId { get; set; }
    }
}
