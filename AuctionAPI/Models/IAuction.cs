namespace AuctionAPI.Models
{
    public interface IAuction
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinPrice { get; set; }
        public int CurrentMaxBid { get; set; }
    }
}
