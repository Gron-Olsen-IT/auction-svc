
using Moq;
using AuctionAPI.Services;
using AuctionAPI.Models;
using Microsoft.Extensions.Logging;


namespace AuctionAPI.Tests;

public class ServiceUnitTests
{

    Mock<IAuctionRepo> _mockAuctionRepo;
    private IAuctionService _auctionService;
    private IInfraRepo _infraRepo;

    Mock<ILogger<AuctionService>> _mockLogger = new Mock<ILogger<AuctionService>>();

    private List<Auction> auctions = new List<Auction>{
        new(new(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), 100, 500, "TestProductId1", "TestEmployeeId1", 1)),
        new(new(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), 200, 500, "TestProductId2", "TestEmployeeId2", 2)),
        new(new(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), 300, 500, "TestProductId3", "TestEmployeeId3", 3)),
        new(new(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), 400, 500, "TestProductId4", "TestEmployeeId4", 4)),
        new(new(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), 0, 500, "TestProductId5", "TestEmployeeId5", 5))
    };


    [SetUp]
    public void Setup()
    {
        _mockAuctionRepo = new Mock<IAuctionRepo>();
        _mockAuctionRepo.Setup(repo => repo.Get()).ReturnsAsync(auctions);

        string testAuctionId = "TestAuctionId1";

        auctions[0].Id = testAuctionId;
        _mockAuctionRepo.Setup(repo => repo.Get()).ReturnsAsync(auctions);
        _mockAuctionRepo.Setup(repo => repo.Get(testAuctionId)).ReturnsAsync(auctions[0]);

        _auctionService = new AuctionService(_infraRepo, _mockAuctionRepo.Object, _mockLogger.Object);
    }

    [Test]
    public async Task GetArgumentlessPass()
    {
        var testAuctions = await _auctionService.Get();
        Assert.That(testAuctions, Is.EqualTo(auctions));
    }

    [Test]
    public async Task GetByIdPass()
    {
        Assert.That(await _auctionService.Get("TestAuctionId1"), Is.EqualTo(auctions[0]));
    }
}