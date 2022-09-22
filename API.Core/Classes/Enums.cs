using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace API.Core.Classes
{
    public class Enums
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CategoryType
        {
            None,
            Residential,
            Rental,
            Land,
            Rural
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusType
        {
            None,
            Current,
            Withdrawn,
            Sold,
            Leased,
            OffMarket,
            Deleted
        }

        public enum EnumDB
        {
            TEST
        }

        public enum DbAccessLevel
        {
            /// <summary>
            /// Read-only access
            /// </summary>
            READ,
            /// <summary>
            /// Read-write access
            /// </summary>
            WRITE
        }
    }
}
