using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Dtos
{
    public class PostContactForContactDataDto
    {
        [Required]
        public int ContactId { get; set; }
        [Required]
        public IEnumerable<PostContactDataForContactDto> ContactData { get; set; }
    }
}
