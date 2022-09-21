using API.Core;
using Microsoft.Extensions.Configuration;
using Xunit;
using API.Core.Models;

namespace API.Tests
{
    public class ListManagerTest
    {
        private IConfiguration _configuration;

        [Fact]
        public void GetResidentialListings()
        {
            _configuration = GetConfiguration();
            string subsurb = "Southbank";

            var listmanager = new ListManager(_configuration);

            var results = listmanager.GetListings(subsurb, Enums.CategoryType.Residential, Enums.StatusType.Current, 0, 50);
            Assert.NotNull(results);
        }

        [Fact]
        public void GetRentalListings()
        {
            _configuration = GetConfiguration();
            string subsurb = "Kew";

            var listmanager = new ListManager(_configuration);

            var results = listmanager.GetListings(subsurb, Enums.CategoryType.Rental, Enums.StatusType.Current, 0, 50);
            Assert.NotNull(results);
        }

        public IConfiguration GetConfiguration()
        {
            if (_configuration == null)
            {
                var builder = new ConfigurationBuilder().AddJsonFile($"testsettings.json", optional: false);
                _configuration = builder.Build();
            }

            return _configuration;
        }
    }
}
