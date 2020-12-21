using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsApi.Dtos;
using ContactsApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
        public async Task<ActionResult<IEnumerable<ContactDto>>> Get() =>
            Ok(await _addressBookService.GetContactsAsync(null));

        /// <summary>
        /// For given ID gets a single contact defined in the address book with his corresponding contact data.
        /// </summary>
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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactDto>> Get(int id)
        {
            List<ContactDto> contacts = await _addressBookService.GetContactsAsync(id);

            ActionResult response;

            if (contacts.Count == 1)
                response = Ok(contacts);
            else
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Contact not found",
                    Detail = $"Contact with ID {id} doesn't exist.",
                    Instance = HttpContext.Request.Path
                };

                response = new ObjectResult(problemDetails)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            return response;
        }

        /// <summary>
        /// Creates a new contact with contact data if given in the address book.
        /// </summary>
        /// <returns>A newly created contact with an array of contact data.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Contacts
        ///     {
        ///         "firstName": "Keanu",
        ///         "surname": "Reeves",
        ///         "dateOfBirth": "1964-09-02T00:00:00",
        ///         "street": "Linda Ave.",
        ///         "addressNumber": "8106",
        ///         "postcode": "12302",
        ///         "city": "Schenectady",
        ///         "country": "New York, US",
        ///         "contactData": [
        ///             {
        ///                 "contactDataType": "PHONE",
        ///                 "contactDataValue": "0900000000"
        ///             },
        ///             {
        ///                 "contactDataType": "PHONE",
        ///                 "contactDataValue": "0900000001"
        ///             },
        ///             {
        ///                 "contactDataType": "MAIL",
        ///                 "contactDataValue": "keanu.reeves@mail.com"
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="201">Contact successfully created</response>
        /// <response code="400">Problem with given contact for creation</response>
        /// <response code="500">Problem with the service</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post(PostContactDto postContactDto)
        {
            ActionResult response;

            try
            {
                ContactDto contact = await _addressBookService.PostContactAsync(postContactDto);

                response = Created($"{Request.GetEncodedUrl()}/{contact.Id}", contact);
            }
            catch (DbUpdateException ex)
            {
                ProblemDetails problemDetails;

                if (ex.GetBaseException() is PostgresException postgresException && postgresException.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Contact already exists",
                        Detail = "Given contact already exists in the address book.",
                        Instance = HttpContext.Request.Path
                    };

                    response = new ObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" },
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "There was an error during the saving process",
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

        /// <summary>
        /// Removes an existing contact with contact data from the address book.
        /// </summary>
        /// <returns>No content.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/Contacts
        ///     {
        ///         "id": 6,
        ///         "createdOrUpdated": "2020-12-20T23:20:59.2401"
        ///     }
        /// </remarks>
        /// <response code="204">Contact successfully deleted</response>
        /// <response code="400">Contact with given ID doesn't exist</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(DeleteContactDto deleteContactDto)
        {
            ActionResult response;

            try
            {
                _ = await _addressBookService.DeleteContactAsync(deleteContactDto);

                response = NoContent();
            }
            catch (DbUpdateException ex)
            {
                ProblemDetails problemDetails;

                if (ex.GetBaseException() is DbUpdateConcurrencyException)
                {
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Contact maybe doesn't exists",
                        Detail = $"Contact with ID {deleteContactDto.Id} isn't in the address book or has changed since fetching data.",
                        Instance = HttpContext.Request.Path
                    };

                    response = new ObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" },
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "There was an error during the deleting process",
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

        /// <summary>
        /// Updates an existing contact without contact data in the address book.
        /// </summary>
        /// <returns>Updated contact without contact data.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Contacts
        ///     {
        ///	        "id": 17,
        ///	        "firstName": "Boris",
        ///	        "surname": "Reeves",
        ///	        "dateOfBirth": "1964-09-02T00:00:00",
        ///	        "street": "Linda Ave.",
        ///	        "addressNumber": "8106",
        ///	        "postcode": "12302",
        ///	        "city": "Schenectady",
        ///	        "country": "New York, US",
        ///	        "createdOrUpdated": "2020-12-21T03:24:57.82887"
        ///     }
        /// </remarks>
        /// <response code="200">Contact successfully updated</response>
        /// <response code="400">Contact with given ID doesn't exist</response>
        /// <response code="500">Problem with the service</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put(PutContactDto putContactDto)
        {
            ActionResult response;

            try
            {
                response = Ok(await _addressBookService.PutContactAsync(putContactDto));
            }
            catch (Exception ex)
            {
                ProblemDetails problemDetails;

                if (ex.GetBaseException() is InvalidOperationException)
                {
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Contact maybe doesn't exists",
                        Detail = $"Contact with ID {putContactDto.Id} isn't in the address book or has changed since fetching data.",
                        Instance = HttpContext.Request.Path
                    };

                    response = new ObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" },
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "There was an error during the updating process",
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
