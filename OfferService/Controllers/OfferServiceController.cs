using ClassLibrary.Classes;
using ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OfferService.Interfaces;

namespace OfferService.Controllers;

[ApiController]
[Route("[controller]")]
public class OfferServiceController : ControllerBase, IOfferService
{
    private readonly ILogger<OfferServiceController> _logger;
    private readonly IDataProvider _dataProvider;

    public OfferServiceController(ILogger<OfferServiceController> logger, IDataProvider dataProvider)
    {
        _logger = logger;
        _dataProvider = dataProvider;
    }

    [HttpPost("Create")]
    public Offer? Create([FromBody]Offer offer)
    {
        return _dataProvider.Create(offer);
    }
    
    [HttpGet("Get/{id}")]
    public Offer? Get(Guid id)
    {
        return _dataProvider.Get(id);
    }
    
    [HttpGet("List/{jobId}")]
    public IEnumerable<Offer> List(Guid jobId)
    {
        var offers = _dataProvider.List(jobId);
        return offers.ToArray();
    }

    [HttpPut("Update")]
    public Offer? Update([FromBody]Offer offer)
    {
        return _dataProvider.Update(offer);
    }

    [HttpDelete("Delete/{id}")]
    public bool Delete(Guid id)
    {
        return _dataProvider.Delete(id);
    }
}
