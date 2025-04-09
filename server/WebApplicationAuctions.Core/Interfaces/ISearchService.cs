using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Data.Dto;
using WebApplicationAuctions.Domain.DTO_s.Responses;

namespace WebApplicationAuctions.Core.Interfaces
{
    public interface ISearchService
    {
        public Task<SearchAuctionResponse> SearchAuctionById(int id);

        public Task<IEnumerable<SearchDto>> SearchAuctions(string keyword);
    }
}
