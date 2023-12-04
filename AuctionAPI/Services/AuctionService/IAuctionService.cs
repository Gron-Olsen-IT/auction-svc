using Microsoft.AspNetCore.Mvc;
using AuctionAPI.Models;

namespace AuctionAPI.Services;

public interface IAuctionService
{
    public Task<List<Auction>> Get();
    public Task<Auction> Get(string id);
    public Task<List<Auction>> GetActiveAuctions();
    public Task<List<Auction>> GetAuctionsLast5Minutes();
    public Task<List<AuctionProductDTO>> GetProductIds(List<string> ids);
    public Task<Auction> Post([FromBody] AuctionDTO productDTO);
    public Task<Auction> Put([FromBody] Auction auction);

}