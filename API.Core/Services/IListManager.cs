using API.Core.Classes;
using API.Core.Models;
using System.Threading.Tasks;
using static API.Core.Classes.Enums;

namespace API.Core
{
    public interface IListManager
    {
        // Return Task for async function
        Task<PagedResult<Listing>> GetListings(string suburb, CategoryType categoryType, StatusType statusType, int skip, int take);
    }
}
