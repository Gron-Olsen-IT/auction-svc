using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionAPI.Models;


public record AuctionExternalDto : IAuction
{

    public AuctionExternalDto(DateTime startDate, DateTime endDate, int minPrice, int currentMaxBid, string description, string name)
    {
        StartDate = startDate;
        EndDate = endDate;
        MinPrice = minPrice;
        CurrentMaxBid = currentMaxBid;
        Description = description;
        Name = name;
    }  

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MinPrice { get; set; }
    public int CurrentMaxBid { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }

}
