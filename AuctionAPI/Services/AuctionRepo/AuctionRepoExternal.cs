using Microsoft.AspNetCore.Mvc;
using AuctionAPI.Models;
using MongoDB.Driver;
using System.Net;
using ZstdSharp;
using System.Security.Cryptography;

namespace AuctionAPI.Services;

public class AuctionRepoExternal : IAuctionRepo
{

    private readonly List<AuctionExternalDto> _collection;

    public AuctionRepoExternal()
    {
        _collection = new()
        {
            {new(DateTime.Now, DateTime.Now.AddDays(1), 100, 200, "Description 1", "Auction 1") },
            {new(DateTime.Now, DateTime.Now.AddDays(1), 200, 400, "Description 2", "Auction 2") },
            {new(DateTime.Now, DateTime.Now.AddDays(1), 300, 600, "Description 3", "Auction 3") },
            {new(DateTime.Now, DateTime.Now.AddDays(1), 400, 800, "Description 4", "Auction 4") }

        };
    }
    public async Task<List<IAuction>> Get()
    {
        try
        {
            List<IAuction> returnAuctions = _collection.Where(x => x is IAuction).ToList<IAuction>();
            await Task.Delay(RandomNumberGenerator.GetInt32(500, 2000));
            if (returnAuctions.Count == 0)
            {
                throw new Exception("No auctions found");
            }
            return returnAuctions;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);

        }
    }
    public async Task<Auction> Get(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Auction>> GetActiveAuctions()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Auction>?> GetExpiredActiveAuctions()
    {
        throw new NotImplementedException();
    }

    public async Task<List<AuctionProductDTO>> GetProductIds(List<string> auctionIds)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetMinPrice(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<Auction> Post(Auction auction)
    {

        throw new NotImplementedException();
    }

    public async Task<Auction> Put(Auction auction)
    {
        throw new NotImplementedException();
    }

    public async Task<Auction> PatchMaxBid(string id, int maxBid)
    {
        throw new NotImplementedException();
    }

    public async Task<Auction> PatchStatus(string id, int status)
    {
        throw new NotImplementedException();
    }




}