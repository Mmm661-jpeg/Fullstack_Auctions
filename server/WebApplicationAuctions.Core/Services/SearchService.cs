using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Data.Dto;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DTO_s.Responses;

namespace WebApplicationAuctions.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepo repo;

        public SearchService(ISearchRepo repo)
        {
            this.repo = repo;
        }
        public async Task<IEnumerable<SearchDto>> SearchAuctions(string keyword)
        {
            var result = await repo.SearchAuctions(keyword);
            return result;
        }
            

        
        public async Task<SearchAuctionResponse> SearchAuctionById(int id)
        {
            var result = await repo.SearchAuctionById(id);
            return result;

           
        }
    }
}
