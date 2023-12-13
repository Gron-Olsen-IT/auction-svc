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

    public async Task<List<Auction>> GetExpiredActiveAuctions()
    {
        try
        {
            List<Auction> returnAuctions = await _collection.Find(auction => auction.EndDate < DateTime.Now && auction.Status == 1).ToListAsync();
            if (returnAuctions.Count == 0)
            {
                throw new Exception("No expired auctions with status active found");
            }
            return returnAuctions;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);

        }
    }

    public async Task<List<AuctionProductDTO>> GetProductIds(List<string> auctionIds)
    {
        try
        {
            List<Auction> auctions = await _collection.Find(auction => auctionIds.Contains(auction.Id!)).ToListAsync();
            
            List<AuctionProductDTO> returnIds = new();
            if (auctions.Count == 0)
                
            {
                throw new Exception("No auctions found");
            }

            foreach (var auction in auctions)
            {
                returnIds.Add(new AuctionProductDTO(auction.Id, auction.ProductId));
            }
            return returnIds!;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<int> GetMinPrice(string id)
    {
        try
        {
            Auction auction = await _collection.Find(a => a.Id == id).FirstOrDefaultAsync();
            if (auction == null)
            {
                throw new Exception("Auction not found");
            }
            return auction.MinPrice;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Auction> Post(Auction auction)
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

    public async Task<Auction> Put(Auction auction)
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

    public async Task<Auction> PatchMaxBid(string id, int maxBid)
    {
        try
        {
            Auction auction = _collection.Find(a => a.Id == id).FirstOrDefault();
            if (auction == null)
            {
                throw new Exception("Auction not found");
            }
            auction.CurrentMaxBid = maxBid;
            Console.WriteLine("Current Max Bid patch value: " + auction.CurrentMaxBid);
            await _collection.ReplaceOneAsync(a => a.Id == id, auction);
            return auction;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Auction> PatchStatus(string id, int status){
        try
        {
            Auction auction = _collection.Find(a => a.Id == id).FirstOrDefault();
            if (auction == null)
            {
                throw new Exception("Auction not found");
            }
            auction.Status = status;
            await _collection.ReplaceOneAsync(a => a.Id == id, auction);
            return auction;
        }
        catch (Exception e)
        {
            throw new Exception("Could not patch auction status " + e.Message);
        }
    }




}