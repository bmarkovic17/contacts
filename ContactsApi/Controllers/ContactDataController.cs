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
        /// For given contact ID gets an array of contact data defined in the address book.
        /// </summary>
        /// <param name="contactId">Contact ID for which contact data is fetched.</param>
        /// <returns>An array of contact data.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/ContactsData?contactId=1
        ///     {
        ///     }
        /// </remarks>
        /// <response code="200">Contact data returned successfully</response>
        /// <response code="404">Contact not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ContactDataDto>>> Get(int contactId)
        {
            ActionResult response;
            
            try
            {
                response = Ok(await _addressBookService.GetContactDataAsync(contactId));

            }
            catch (Exception ex)
            {
                ProblemDetails problemDetails;

                if (ex.GetBaseException() is InvalidOperationException)
                {
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Contact not found",
                        Detail = $"Contact with ID {contactId} doesn't exist.",
                        Instance = HttpContext.Request.Path
                    };

                    response = new ObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" },
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                else
                {
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "There was an error during the fetching process",
                        Instance = HttpContext.Request.Path
                    };

                    response = new ObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" },
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
            }

            return response;
        }
    }
}
