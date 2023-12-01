using Microsoft.AspNetCore.Mvc;
using AuctionAPI.Models;
using MongoDB.Driver;
using System.Net;
using ZstdSharp;

namespace AuctionAPI.Services;

public class AuctionRepoMongo : IAuctionRepo
{

    private readonly IMongoCollection<Auction> _collection;

    public AuctionRepoMongo()
    {
        string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "mongodb://admin:1234@localhost:27017/";
        var mongoDatabase = new MongoClient(connectionString).GetDatabase("auction_db");
        _collection = mongoDatabase.GetCollection<Auction>("auctions");
    }
    public async Task<List<Auction>> Get()
    {
        try
        {
            List<Auction> returnAuctions = await _collection.Find(auction => true).ToListAsync();
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
        try
        {
            Auction returnAuction = await _collection.Find<Auction>(auction => auction.Id == id).FirstOrDefaultAsync();
            if (returnAuction == null)  
            {
                throw new Exception("Auction not found");
            }
            return returnAuction;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Auction>> GetActiveAuctions()
    {
        try
        {
            List<Auction> returnAuctions = await _collection.Find(auction => auction.EndDate > DateTime.Now).ToListAsync();
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

    public async Task<List<Auction>> GetAuctionsLast5Minutes()
    {
        try
        {
            List<Auction> returnAuctions = await _collection.Find(auction => auction.EndDate < DateTime.Now.AddMinutes(5)).ToListAsync();
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

    public async Task<List<string>> GetProductIds(List<string> ids)
    {
        try
        {
            List<string> returnIds = await _collection.Find(auction => ids.Contains(auction.Id!)).Project(auction => auction.ProductId).ToListAsync();
            if (returnIds.Count == 0)
            {
                throw new Exception("No auctions found");
            }
            return returnIds;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Auction> Post([FromBody] Auction auction)
    {

        try
        {
            await _collection.InsertOneAsync(auction);
            return auction;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Auction> Put([FromBody] Auction auction)
    {
        try
        {
            await _collection.ReplaceOneAsync(a => a.Id == auction.Id, auction);
            return auction;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }




}