using BPDTS_Test_API.Models;
using BPDTS_Test_API.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BPDTS_Test_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class TestAnswerController : ControllerBase
    {
        private readonly ILogger<TestAnswerController> _logger;
        private readonly IBPDTSTestApiService _mainService;

        public TestAnswerController(ILogger<TestAnswerController> logger, IBPDTSTestApiService mainService)
        {
            _logger = logger;
            _mainService = mainService;
        }

        /// <summary>
        /// A GET request that returns a list of users from the London area.
        ///
        /// </summary>
        /// <remarks>
        /// This list is formed from concatinating calls from external API methods '/city/{city}/users' and '/users' - filtering by city name 'London' and coordinates within 50 miles of London centre.
        ///
        /// Sample response:
        ///
        ///     GET users/london
        ///     [
        ///         {
        ///             "id": "135",
        ///             "first_name": "Mechelle",
        ///             "last_name": "Boam",
        ///             "email": "mboam3q@thetimes.co.uk",
        ///             "ip_address": "113.71.242.187",
        ///             "latitude": "-6.5115909",
        ///             "longitude": "105.652983"
        ///           },
        ///           {
        ///             "id": "396",
        ///             "first_name": "Terry",
        ///             "last_name": "Stowgill",
        ///             "email": "tstowgillaz@webeden.co.uk",
        ///             "ip_address": "143.190.50.240",
        ///             "latitude": "-6.7098551",
        ///             "longitude": "111.3479498"
        ///           }
        ///     ]
        ///
        /// </remarks>
        /// <response code="404">If the API calls return null</response>
        /// <response code="400">If there is a problem internally in the controller</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [Route("users/london")]
        public async Task<IActionResult> GetLondonUsersByCityNameAndCoordinates()
        {
            try
            {
                List<User> londonUsers = await _mainService.GetLondonUsersByCityNameAndCoordinates();
                if (londonUsers == null)
                {
                    return NotFound();
                }

                return Ok(londonUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"API: Exception thrown when retreiving londons users by city name and coordinates: {ex}");
            }

            return BadRequest();
        }

        /// <summary>
        /// A GET request that returns a list of users from the London area - By city name.
        ///
        /// </summary>
        /// <remarks>
        /// This list is formed from concatinating calls from external API method '/city/{city}/users' - passing 'London' as the city name.
        ///
        /// Sample response:
        ///
        ///     GET users/london/citynameonly
        ///     [
        ///         {
        ///             "id": "135",
        ///             "first_name": "Mechelle",
        ///             "last_name": "Boam",
        ///             "email": "mboam3q@thetimes.co.uk",
        ///             "ip_address": "113.71.242.187",
        ///             "latitude": "-6.5115909",
        ///             "longitude": "105.652983"
        ///           },
        ///           {
        ///             "id": "396",
        ///             "first_name": "Terry",
        ///             "last_name": "Stowgill",
        ///             "email": "tstowgillaz@webeden.co.uk",
        ///             "ip_address": "143.190.50.240",
        ///             "latitude": "-6.7098551",
        ///             "longitude": "111.3479498"
        ///           }
        ///     ]
        ///
        /// </remarks>
        /// <response code="404">If the API calls return null</response>
        /// <response code="400">If there is a problem internally in the controller</response>
        [HttpGet]
        [Route("users/london/citynameonly")]
        public async Task<IActionResult> GetLondonUsersByCityName()
        {
            try
            {
                List<User> londonUsers = await _mainService.GetUsersByCity("London");
                if (londonUsers == null)
                {
                    return NotFound();
                }

                return Ok(londonUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"API: Exception thrown when retreiving londons users by city name: {ex}");
            }

            return BadRequest();
        }

        /// <summary>
        /// A GET request that returns a list of users from the London area - By coordinates.
        ///
        /// </summary>
        /// <remarks>
        /// This list is formed from concatinating calls from external API method '/users' - filtering by coordinates within 50 miles of London centre.
        ///
        /// Sample response:
        ///
        ///     GET users/london/coordinatesonly
        ///     [
        ///         {
        ///             "id": "135",
        ///             "first_name": "Mechelle",
        ///             "last_name": "Boam",
        ///             "email": "mboam3q@thetimes.co.uk",
        ///             "ip_address": "113.71.242.187",
        ///             "latitude": "-6.5115909",
        ///             "longitude": "105.652983"
        ///           },
        ///           {
        ///             "id": "396",
        ///             "first_name": "Terry",
        ///             "last_name": "Stowgill",
        ///             "email": "tstowgillaz@webeden.co.uk",
        ///             "ip_address": "143.190.50.240",
        ///             "latitude": "-6.7098551",
        ///             "longitude": "111.3479498"
        ///           }
        ///     ]
        ///
        /// </remarks>
        /// <response code="404">If the API calls return null</response>
        /// <response code="400">If there is a problem internally in the controller</response>
        [HttpGet]
        [Route("users/london/coordinatesonly")]
        public async Task<IActionResult> GetLondonUsersByCoordinates()
        {
            try
            {
                List<User> usersWithinLondonLimit = await _mainService.GetUsersByLondonProximity();
                if (usersWithinLondonLimit == null)
                {
                    return NotFound();
                }

                return Ok(usersWithinLondonLimit);
            }
            catch (Exception ex)
            {
                _logger.LogError($"API: Exception thrown when retreiving londons users by coordinates: {ex}");
            }

            return BadRequest();
        }
    }
}