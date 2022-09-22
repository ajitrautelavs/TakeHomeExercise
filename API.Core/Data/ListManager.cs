using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Classes;
using API.Core.Data;
using API.Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using static API.Core.Classes.Enums;

namespace API.Core
{
    public class ListManager : IListManager
    {
        private readonly IConfiguration _configuration;

        public ListManager(IConfiguration config)
        { 
            _configuration = config;
        }

        // Make below function async
        public async Task<PagedResult<Listing>> GetListings(string suburb, CategoryType categoryType, StatusType statusType, int skip, int take)
        {            
            var listings = new List<Listing>();
            var total = 0;

            var filter = categoryType != CategoryType.None ? $" AND CategoryType = {(int)categoryType } " : string.Empty;
            // Bugfix to retain previous filter value
            filter += statusType != StatusType.None ? $" AND StatusType = {(int)statusType } " : string.Empty;

            var sql = $@" SELECT count(ListingId) FROM [Backend-TakeHomeExercise].dbo.Listings WITH(NOLOCK)
                                WHERE Suburb = @suburb { filter} ;

                                SELECT ListingId, StreetNumber, Street, Suburb, State, Postcode, DisplayPrice, Title, CategoryType, StatusType
                                FROM [Backend-TakeHomeExercise].dbo.Listings WITH(NOLOCK)
                                WHERE Suburb = @suburb { filter} 
                                ORDER BY ListingId
                                OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ;
                            ";
                            
            var dbManager = new DbManager(EnumDB.TEST, DbAccessLevel.READ, _configuration);

            using (var db = dbManager.GetOpenConnection())
            {
                var cmd = new CommandDefinition(sql, new { suburb, cattype = (int)categoryType, statusType = (int)statusType, skip, take });
                var multi = db.QueryMultiple(cmd);

                total = multi.Read<int>().FirstOrDefault();
                //Use ReadAsync
                listings = (List<Listing>)await multi.ReadAsync<Listing>();
            }

            if (total == 0)
                return null;

            return new PagedResult<Listing>(skip, total, listings);            
        }
    }
}
