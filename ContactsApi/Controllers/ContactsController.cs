using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Models;
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
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService) =>
            _contactsService = contactsService;

        /// <summary>
        /// Gets all contacts defined in the address book.
        /// </summary>
        /// <returns>An array of contacts</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Contacts
        ///     {
        ///     }
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts() =>
            await _contactsService.GetUsersAsync();
    }
}
