using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Data.Dto;
using WebApplicationAuctions.Data.Repository;
using WebApplicationAuctions.Domain.DomainModels;
using WebApplicationAuctions.Domain.DTO_s.Responses;

namespace WebApplicationAuctions.Data.Interfaces
{
    public interface ISearchRepo
    {
        public Task<IEnumerable<SearchDto>> SearchAuctions(string keyword);
        public Task<SearchAuctionResponse> SearchAuctionById(int id);
    }
}
