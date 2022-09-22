using System;
using System.Linq;
using System.Threading.Tasks;
using API.Core;
using API.Core.Classes;
using API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static API.Core.Classes.Enums;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingsController : ControllerBase
    {
        private readonly ILogger<ListingsController> _logger;
        // Below object probably not needed
        //private readonly IConfiguration _configuration;
        private readonly IListManager _listManager;

        public ListingsController(ILogger<ListingsController> logger, IListManager listManager)
        {
            _logger = logger;
            // Below object probably not needed, removed from parameters
            //_configuration = config;

            // Inject IListManager in constructor
            _listManager = listManager;
        }

        //Default route
        [HttpGet]
        public IActionResult EmptyRoute()
        {
            return BadRequest(new { message = "Route is not in listings/{suburb} format" });
        }

        // Suburb in route
        [HttpGet("{suburb}")]
        public async Task<IActionResult> GetListings([FromRoute] string suburb, [FromQuery] CategoryType categoryType = CategoryType.None,
            [FromQuery] StatusType statusType = StatusType.None, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            string errMsg;

            if (string.IsNullOrEmpty(suburb))
            {
                // Additional information in error message
                errMsg = "No Suburb provided in route listings/{suburb}";
                // May need to log error
                _logger.LogError(errMsg);
                // Return message in Json
                return BadRequest(new { message = errMsg });
            }

            // Comment out following as already initialised in constructor
            //_listManager = new ListManager(_configuration);

            PagedResult<Listing> listings = await _listManager.GetListings(suburb, categoryType, statusType, skip, take);

            errMsg = $"No listings found for suburb {suburb}";
            if (listings != null && listings.Results != null)
            {
                if (listings.Results.Any() == false)
                {
                    
                    // May need to log
                    _logger.LogWarning(errMsg);
                    
                    // Instead of throwing exception return NotFound result
                    //throw new Exception("No results");
                    return NotFound(new { message = errMsg });
                }
                else
                {
                    return Ok(JsonConvert.SerializeObject(listings));
                }
            }

            // May need to log
            _logger.LogWarning(errMsg);
            // Return message in Json
            return NotFound(new { message = errMsg });
        }

        // Some error handling route
        [Route("error/{code}")]
        public IActionResult Error(int code)
        {
            switch (code)
            {
                case 404:
                    return NotFound(new { message = "Resource not found" });
                case 500:
                    return NotFound(new { message = "An unhandled error occurred" });
                default:
                    return null;
            }
        }
    }
}
