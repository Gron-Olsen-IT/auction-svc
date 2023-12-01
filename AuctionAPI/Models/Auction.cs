namespace AuctionAPI.Models;


public record Auction{
    public string Id { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public int MinPrice { get; set; }
    public string ProductId { get; set; }
    public string EmployeeId { get; set; }
    public int Status { get; set; }
}