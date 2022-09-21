using System;
using System.Linq;
using API.Core;
using API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static API.Core.Models.Enums;

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

        // Suburb in route
        [HttpGet("{suburb}")]
        public IActionResult GetListings([FromRoute] string suburb, [FromQuery] CategoryType categoryType = CategoryType.None,
            [FromQuery] StatusType statusType = StatusType.None, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            if (string.IsNullOrEmpty(suburb))
            {
                // May need to log error
                _logger.LogError($"No Suburb provided.");
                // Additional information in error message
                return BadRequest("No Suburb provided in route api/listings/{suburb}");
            }

            // Comment out following as already initialised in constructor
            //_listManager = new ListManager(_configuration);

            PagedResult<Listing> listings = _listManager.GetListings(suburb, categoryType, statusType, skip, take);

            string errMsg = $"No listings found for suburb {suburb}";
            if (listings != null && listings.Results != null)
            {
                if (listings.Results.Any() == false)
                {
                    
                    // May need to log
                    _logger.LogWarning(errMsg);
                    
                    // Instead of throwing exception return NotFound result
                    //throw new Exception("No results");
                    return NotFound(errMsg);
                }
                else
                {
                    return Ok(JsonConvert.SerializeObject(listings));
                }
            }

            // May need to log
            _logger.LogWarning(errMsg);
            return NotFound(errMsg);
        }
    }
}
