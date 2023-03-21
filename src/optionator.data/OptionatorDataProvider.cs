
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using optionator.core;

namespace optionator.data;

public class OptionatorDataProvider
{
    private readonly OptionatorRepository _optionatorRepository;
    private readonly List<Optionator> _optionators;

    public OptionatorDataProvider(OptionatorRepository optionatorRepository)
    {
        _optionatorRepository = optionatorRepository;
        _optionators = _optionatorRepository.GetOptionatorsAsync().Result.Optionators;
    }

    public List<Optionator> GetOptionators()
    {
        return _optionators;
    }

    public Optionator RandomOptionator()
    {
        Random r = new Random();
        int next = r.Next(0, _optionators.Count);
        return _optionators[next];        
    }
}