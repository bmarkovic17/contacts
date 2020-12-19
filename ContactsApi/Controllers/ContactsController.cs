using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Dtos;
using ContactsApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContactsController : ControllerBase
    {
        private readonly IAddressBookService _addressBookService;

        public ContactsController(IAddressBookService addressBookService) =>
            _addressBookService = addressBookService;

        /// <summary>
        /// Gets all contacts defined in the address book with their's corresponding contact data.
        /// </summary>
        /// <returns>An array of contacts with an array of contact data.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Contacts
        ///     {
        ///     }
        /// </remarks>
        /// <response code="200">Data returned successfully (no data is also a correct response)</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts() =>
            Ok(await _addressBookService.GetContactsAsync());
    }
}
