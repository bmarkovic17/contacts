using System;

namespace ContactsApi.Dtos
{
    public class ContactDataDto
    {
        public int Id { get; set; }
        public string ContactDataType { get; set; }
        public string ContactDataValue { get; set; }
        public DateTime CreatedOrUpdated { get; set; }
    }
}
