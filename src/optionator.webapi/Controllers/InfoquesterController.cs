using Microsoft.AspNetCore.Mvc;
using optionator.core;
using optionator.data;

namespace optionator.webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class InfoquesterController : ControllerBase
{
    private readonly InfoquesterRepository _infoquesterRepository;

    private readonly ILogger<InfoquesterController> _logger;

    public InfoquesterController(
        ILogger<InfoquesterController> logger,
        InfoquesterRepository infoquesterRepository)
    {
        _logger = logger;
        _infoquesterRepository = infoquesterRepository;
    }

    [HttpGet(Name = "GetInfoquesters")]
    public async IAsyncEnumerable<Infoquester> Get()
    {
        var result = await _infoquesterRepository.GetInfoquestersAsync();

        foreach (var infoquester in result.Infoquesters)
        {
            yield return infoquester;
        }
    }
}
