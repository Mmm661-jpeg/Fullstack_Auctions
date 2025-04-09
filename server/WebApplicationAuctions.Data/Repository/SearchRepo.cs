using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Data.Dto;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Responses;

namespace WebApplicationAuctions.Data.Repository
{
    public class SearchRepo : ISearchRepo
    {
        private readonly IAuctionsContext _context;

        public SearchRepo(IAuctionsContext context)
        {
            _context = context;
        }


        
        public async Task <IEnumerable<SearchDto>> SearchAuctions(string keyword)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Keyword", keyword);
            using (var db = _context.GetConnection())
            {
                await db.OpenAsync();

                var result = await db.QueryAsync<SearchDto>("SearchAuction",parameters
                    ,commandType: CommandType.StoredProcedure);

                await db.CloseAsync();

                return result;

            }
        }

        public async Task<SearchAuctionResponse> SearchAuctionById(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@AuctionId", id);

            using (var db = _context.GetConnection())
            {
                await db.OpenAsync();
                var result = await db.QuerySingleAsync<SearchAuctionResponse>("SearchAuctionByID3",
                    parameter, commandType: CommandType.StoredProcedure);

                await db.CloseAsync();

                return result;
            }
        }
    }
}
