using Microsoft.AspNetCore.Mvc;
using AuctionAPI.Models;

namespace AuctionAPI.Services;

public interface IAuctionRepo
{
    public Task<List<Auction>> Get();
    public Task<Auction> Get(string id);
    public Task<List<Auction>> GetActiveAuctions();
    public Task<List<Auction>> GetAuctionsLast5Minutes();
    public Task<List<Tuple<string, string>>> GetProductIds(List<string> ids);
    public Task<Auction> Post([FromBody] Auction auction);
    public Task<Auction> Put([FromBody] Auction auction);

}