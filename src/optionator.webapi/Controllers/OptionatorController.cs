using Microsoft.AspNetCore.Mvc;
using optionator.core;
using optionator.data;

namespace optionator.webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class OptionatorController : ControllerBase
{
    private readonly OptionatorDataProvider _optionatorDataProvider;

    private readonly ILogger<OptionatorController> _logger;

    public OptionatorController(
        ILogger<OptionatorController> logger,
        OptionatorDataProvider optionatorDataProvider)
    {
        _logger = logger;
        _optionatorDataProvider = optionatorDataProvider;
    }

    [HttpGet(Name = "GetOptionators")]
    public IEnumerable<Optionator> Get()
    {
        return _optionatorDataProvider.GetOptionators();
    }

    [HttpGet("random")]
    public Optionator GetRandom()
    {
        return _optionatorDataProvider.RandomOptionator();
    }
}
