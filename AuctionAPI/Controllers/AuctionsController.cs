using Microsoft.AspNetCore.Mvc;
using AuctionAPI.Services;
using AuctionAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace AuctionAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuctionsController : ControllerBase
{

    private readonly ILogger<AuctionsController> _logger;
    private readonly IAuctionService _service;



    public AuctionsController(ILogger<AuctionsController> logger, IAuctionService service)
    {
        _service = service;
        _logger = logger;
        try
        {
            var hostName = System.Net.Dns.GetHostName();
            var ips = System.Net.Dns.GetHostAddresses(hostName);
            var _ipaddr = ips.First().MapToIPv4().ToString();
            _logger.LogInformation(1, $"AuctionService responding from {_ipaddr}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in AuctionsController");
        }

    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _service.Get());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Get");
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            return Ok(await _service.Get(id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Get by id");
            return StatusCode(404, e.Message);
        }
    }

    [Authorize]
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveAuctions()
    {
        try
        {
            return Ok(await _service.GetActiveAuctions());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetActiveAuctions");
            return BadRequest(e.Message);
        }
    }

    [HttpGet("expiredactive")]
    public async Task<IActionResult> GetExpiredActiveAuctions()
    {
        try
        {
            return Ok(await _service.GetExpiredActiveAuctions());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetExpiredActiveAuctions");
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("minprice/{id}")]
    public async Task<IActionResult> GetMinPrice(string id)
    {
        try
        {
            return Ok(await _service.GetMinPrice(id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetMinPrice");
            return BadRequest(e.Message);
        }
    }

    [HttpPost("products")]
    public async Task<IActionResult> GetProductsByAuctionIds(List<string> auctionIds)
    {
        try
        {
            return Ok(await _service.GetProductIds(auctionIds));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetAuctionsByProduct");
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AuctionDTO auctionDTO)
    {
        try
        {
            return Ok(await _service.Post(auctionDTO));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Post");
            return StatusCode(404, e.Message);
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Auction auction)
    {
        try
        {
            return Ok(await _service.Put(auction));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Put");
            return StatusCode(404, e.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchMaxBid(string id, int maxBid)
    {
        try
        {
            return Ok(await _service.PatchMaxBid(id, maxBid));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in PatchMaxBid");
            return StatusCode(404, e.Message);
        }
    }
    
}
