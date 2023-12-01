using System.Net;
using AuctionAPI.Models;

namespace AuctionAPI.Services;

public interface IInfraRepo {

    public Task<List<Tuple<string, string>>> GetMaxBid(List<string> auctionIds);

    /* For Redis DB Cached keyvalues - Future
    public Task<List<Auction>> GetProduct(string auctionId);
    */

    
}