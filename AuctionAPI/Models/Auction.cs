namespace AuctionAPI.Models;


public record Auction{

    public Auction()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public int MinPrice { get; set; }
    public string ProductId { get; set; }
    public string EmployeeId { get; set; }
    public int Status { get; set; }
}

public record AuctionDTO{

    public AuctionDTO(TimeSpan timeSpan, int minPrice, string productId, string employeeId, int status)
    {
        TimeSpan = timeSpan;
        MinPrice = minPrice;
        ProductId = productId;
        EmployeeId = employeeId;
        Status = status;
    }  

    public TimeSpan TimeSpan { get; set; }
    public int MinPrice { get; set; }
    public string ProductId { get; set; }
    public string EmployeeId { get; set; }
    public int Status { get; set; }

}