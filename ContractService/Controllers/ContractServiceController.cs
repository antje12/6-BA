using ClassLibrary.Classes;
using ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ContractService.Interfaces;

namespace ContractService.Controllers;

[ApiController]
[Route("[controller]")]
public class ContractServiceController : ControllerBase, IContractService
{
    private readonly ILogger<ContractServiceController> _logger;
    private readonly IDataProvider _dataProvider;

    public ContractServiceController(ILogger<ContractServiceController> logger, IDataProvider dataProvider)
    {
        _logger = logger;
        _dataProvider = dataProvider;
    }

    [HttpPost("CreateContract")]
    public Contract? CreateContract([FromBody]Contract contract)
    {
        return _dataProvider.Create(contract);
    }

    [HttpGet("GetContractById/{id}")]
    public Contract? GetContractById(Guid id)
    {
        return _dataProvider.Get(id);
    }

    [HttpGet("ListContracts/{userId}")]
    public IEnumerable<Contract> ListContracts(Guid userId)
    {
        return _dataProvider.List(userId);
    }

    [HttpPut("ConcludeContract/{id}")]
    public Contract? ConcludeContract(Guid id)
    {
        // ToDo: Fix
        throw new NotImplementedException();
    }

    [HttpDelete("CancelContract/{id}")]
    public Contract? CancelContract(Guid id)
    {
        // ToDo: Fix
        throw new NotImplementedException();
    }
}
