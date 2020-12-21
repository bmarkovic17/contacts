using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ContactDataController : ControllerBase
    {
        private readonly IAddressBookService _addressBookService;

        public ContactDataController(IAddressBookService addressBookService) =>
            _addressBookService = addressBookService;

        /// <summary>
        /// For given ID gets a single contact defined in the address book with his corresponding contact data.
        /// </summary>
        /// <param name="contactId">Contact ID for which contact data is fetched.</param>
        /// <returns>A contact with an array of contact data.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/Contacts/1
        ///     {
        ///     }
        /// </remarks>
        /// <response code="200">Data returned successfully</response>
        /// <response code="404">Contact not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ContactDataDto>>> Get(int contactId)=>
            Ok(await _addressBookService.GetContactDataAsync(contactId));
    }
}
