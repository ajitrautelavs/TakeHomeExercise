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
        private readonly IConfiguration _configuration;
        private IListManager _listManager;

        public ListingsController(ILogger<ListingsController> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
        }

        [HttpGet("")]
        [Route("")]
        public IActionResult GetListings(string suburb, CategoryType categoryType = CategoryType.None, StatusType statusType = StatusType.None, int skip = 0, int take = 10)
        {
            if (string.IsNullOrEmpty(suburb))
                return BadRequest("No Suburb provided");

            _listManager = new ListManager(_configuration);

            PagedResult<Listing> listings = _listManager.GetListings(suburb, categoryType, statusType, skip, take);

            if (listings != null && listings.Results != null)
            {
                if (!listings.Results.Any())
                {
                    throw new Exception("No results");
                }
                else
                {
                    return Ok(JsonConvert.SerializeObject(listings));
                }
            }

            return NotFound();
        }
    }
}
