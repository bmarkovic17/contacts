using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Creates, updates and deletes contact data in the address book for given contact ID.
        /// </summary>
        /// <returns>Active contact data.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/ContactData
        ///     {
        ///         "contactId": 1,
        ///         "contactData": [
        ///             {
        ///                 "id": 1,
        ///                 "contactDataType": "PHONE",
        ///                 "contactDataValue": "0900000000"
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "contactDataType": "PHONE",
        ///                 "contactDataValue": "0900000001"
        ///             },
        ///             {
        ///                 "id": 3,
        ///                 "contactDataType": "MAIL",
        ///                 "contactDataValue": "keanu.reeves@mail.com"
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="201">Contact data successfully merged</response>
        /// <response code="400">Problem with given contact for creation</response>
        /// <response code="500">Problem with the service</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Merge(PostContactForContactDataDto postContactForContactDataDto)
        {
            ActionResult response;

            try
            {
                PostContactForContactDataDto contactData = await _addressBookService.PostContactDataAsync(postContactForContactDataDto);

                response = Created($"{Request.GetEncodedUrl()}/{postContactForContactDataDto.ContactId}", contactData);
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
                        Detail = $"Contact with ID {postContactForContactDataDto.ContactId} doesn't exist.",
                        Instance = HttpContext.Request.Path
                    };

                    response = new ObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" },
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                else if (ex.GetBaseException() is PostgresException postgresException && postgresException.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Contact data already exists",
                        Detail = "One of given contact data already exists in the address book.",
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
    }
}
