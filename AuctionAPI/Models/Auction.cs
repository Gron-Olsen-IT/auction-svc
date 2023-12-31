using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionAPI.Models;


public record Auction{


    public Auction(AuctionDTO auctionDTO)
    {
        StartDate = auctionDTO.StartDate;
        EndDate = auctionDTO.EndDate;
        MinPrice = auctionDTO.MinPrice;
        CurrentMaxBid = auctionDTO.CurrentMaxBid;
        ProductId = auctionDTO.ProductId;
        EmployeeId = auctionDTO.EmployeeId;
        Status = auctionDTO.Status;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MinPrice { get; set; }
    public int CurrentMaxBid { get; set; }
    public string ProductId { get; set; }
    public string EmployeeId { get; set; }
    public int Status { get; set; }

    [JsonConstructor]
    public Auction(string id, DateTime startDate, DateTime endDate, int minPrice, int currentMaxBid, string productId, string employeeId, int status)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        MinPrice = minPrice;
        CurrentMaxBid = currentMaxBid;
        ProductId = productId;
        EmployeeId = employeeId;
        Status = status;

    }
}

public record AuctionDTO{

    public AuctionDTO(DateTime startDate, DateTime endDate, int minPrice, int currentMaxBid, string productId, string employeeId, int status)
    {
        StartDate = startDate;
        EndDate = endDate;
        MinPrice = minPrice;
        CurrentMaxBid = currentMaxBid;
        ProductId = productId;
        EmployeeId = employeeId;
        Status = status;
    }  

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MinPrice { get; set; }
    public int CurrentMaxBid { get; set; }
    public string ProductId { get; set; }
    public string EmployeeId { get; set; }
    public int Status { get; set; }

}

public record AuctionProductDTO{

    public AuctionProductDTO(string productId, string auctionId)
    {
        ProductId = productId;
        AuctionId = auctionId;
    }
    public string ProductId { get; set; }
    public string AuctionId { get; set; }
}