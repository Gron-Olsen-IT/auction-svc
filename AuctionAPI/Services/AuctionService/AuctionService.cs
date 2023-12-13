using System;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using AuctionAPI.Models;

namespace AuctionAPI.Services;


public class AuctionService : IAuctionService
{
    private readonly IInfraRepo _infraRepo;
    private readonly IAuctionRepo _auctionRepo;
    private readonly ILogger<AuctionService> _logger;



    public AuctionService(IInfraRepo InfraRepo, IAuctionRepo productRepository, ILogger<AuctionService> logger)
    {
        _auctionRepo = productRepository;
        _infraRepo = InfraRepo;
        _logger = logger;
    }

    public Task<List<Auction>> Get()
    {
        try
        {
            return _auctionRepo.Get();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Auction> Get(string id)
    {
        try
        {
            return _auctionRepo.Get(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<List<Auction>> GetActiveAuctions()
    {
        try
        {
            return await _auctionRepo.GetActiveAuctions();
            
        }
        
        
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<Auction>> GetExpiredActiveAuctions()
    {
        try
        {
            return _auctionRepo.GetExpiredActiveAuctions();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    

    public Task<Auction> Post(AuctionDTO auctionDTO)
    {
        try
        {
            Auction auction = new(auctionDTO);
            if (auction.EndDate < DateTime.Now.AddHours(24))
            {
                throw new Exception("Auction end date must be more than 24 hours");
            }
            if (auction.StartDate < DateTime.Now)
            {
                throw new Exception("Auction start date must be in the future");
            }
            if (auction.StartDate > auction.EndDate)
            {
                throw new Exception("Auction start date must be before end date");
            }
            if (auction.CurrentMaxBid != 0)
            {
                throw new Exception("Auction current max bid must be 0");
            }
            if (auction.Status != 1)
            {
                throw new Exception("Auction status must be 1");
            }
            if (auction.ProductId == null || auction.ProductId == "")
            {
                throw new Exception("Auction product id must be set");
            }
            
            return _auctionRepo.Post(auction);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Auction> Put(Auction auction)
    {
        try
        {
            return _auctionRepo.Put(auction);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<AuctionProductDTO>> GetProductIds(List<string> auctionIds)
    {
        
        try {
            return _auctionRepo.GetProductIds(auctionIds);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public Task<int> GetMinPrice(string id)
    {
        try
        {
            return _auctionRepo.GetMinPrice(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Auction> PatchMaxBid(string id, int maxBid)
    {
        try
        {
            return _auctionRepo.PatchMaxBid(id, maxBid);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Auction> PatchStatus(string id, int status)
    {
        try
        {
            return _auctionRepo.PatchStatus(id, status);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


}
