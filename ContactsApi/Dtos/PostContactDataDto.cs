using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Dtos
{
    public class PostContactDataDto
    {
        [Required]
        public string ContactDataType { get; set; }
        [Required]
        public string ContactDataValue { get; set; }
    }
}
