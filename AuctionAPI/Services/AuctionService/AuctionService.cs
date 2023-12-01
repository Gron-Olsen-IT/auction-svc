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
    private readonly IInfraRepo _InfraRepo;
    private readonly IAuctionRepo _auctionRepo;
    private readonly ILogger<AuctionService> _logger;



    public AuctionService(IInfraRepo InfraRepo, IAuctionRepo productRepository, ILogger<AuctionService> logger)
    {
        _auctionRepo = productRepository;
        _InfraRepo = InfraRepo;
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

    public Task<List<Auction>> GetActiveAuctions()
    {
        try
        {
            return _auctionRepo.GetActiveAuctions();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<Auction>> GetAuctionsLast5Minutes()
    {
        try
        {
            return _auctionRepo.GetAuctionsLast5Minutes();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<string>> GetProductIds(List<string> ids)
    {
        try
        {
            return _auctionRepo.GetProductIds(ids);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Auction> Post([FromBody] AuctionDTO auctionDTO)
    {
        try
        {
            Auction auction = new(auctionDTO);
            return _auctionRepo.Post(auction);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Auction> Put([FromBody] Auction auction)
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


}
