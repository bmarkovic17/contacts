using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Dtos
{
    public class PostContactDataForContactDto
    {
        public int? Id { get; set; }
        [Required]
        public string ContactDataType { get; set; }
        [Required]
        public string ContactDataValue { get; set; }
        public DateTime? CreatedOrUpdated { get; set; }
    }
}
