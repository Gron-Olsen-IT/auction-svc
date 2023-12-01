using System.Net;
using System.Security.Cryptography.X509Certificates;
using AuctionAPI.Models;

namespace AuctionAPI.Services;

public class InfraRepoRender : IInfraRepo
{

    public async Task<List<Tuple<string, string>>> GetMaxBid(List<string> auctionIds)
    {
        try
        {
            string bidConnection = Environment.GetEnvironmentVariable("BID_CONNECTION")!;
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(bidConnection + auctionIds);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<Tuple<string, string>> returnTuple =  await response.Content.ReadFromJsonAsync<List<Tuple<string, string>>>();
                return returnTuple!;
            }
            else
            {
               throw new Exception("Error in GetMaxBid from Bid Service");
            }
        }
        catch
        {
            throw new Exception("Error in GetMaxBid from Bid Service");
        }


    }

}
