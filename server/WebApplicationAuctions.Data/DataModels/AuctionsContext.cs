using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Data.Interfaces;

namespace WebApplicationAuctions.Data.DataModels
{
    public class AuctionsContext : IAuctionsContext
    {
        private readonly string _connectionString;
        public AuctionsContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Auctionssajt");
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
