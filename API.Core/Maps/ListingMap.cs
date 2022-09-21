using API.Core.Models;
using Dapper.FluentMap.Mapping;

namespace API.Core.Maps
{
    public class ListingMap : EntityMap<Listing>
    {
        public ListingMap()
        {
            Map(a => a.ListingId).ToColumn("ListingId");
            Map(a => a.StreetNumber).ToColumn("StreetNumber");
            Map(a => a.Street).ToColumn("Street");
            Map(a => a.Suburb).ToColumn("Suburb");
            Map(a => a.State).ToColumn("State");
            Map(a => a.Postcode).ToColumn("Postcode");
            Map(a => a.CategoryType).ToColumn("CategoryType");
            Map(a => a.StatusType).ToColumn("StatusType");
            Map(a => a.DisplayPrice).ToColumn("DisplayPrice");
            Map(a => a.Title).ToColumn("Title");
        }
    }
}
