using API.Core.Maps;
using Dapper.FluentMap;

namespace API.Core
{
    public class Startup
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ListingMap());
            });
        }
    }
}
