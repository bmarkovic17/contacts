using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Dtos
{
    public class PostContactDataForContactDto
    {
        [Required]
        public int ContactId { get; set; }
        [Required]
        public IEnumerable<PostContactDataDto> ContactData { get; set; }
    }
}
