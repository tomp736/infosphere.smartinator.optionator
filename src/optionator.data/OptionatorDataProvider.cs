
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using optionator.core;

namespace optionator.data;

public class OptionatorDataProvider
{
    private readonly List<OptionatorRepository> _optionatorRepositories;
    private readonly Lazy<List<Optionator>> _optionatorsLazy;

    public OptionatorDataProvider(List<OptionatorRepository> optionatorRepositories)
    {
        _optionatorRepositories = optionatorRepositories;
        _optionatorsLazy = new Lazy<List<Optionator>>(() => LoadOptionatorsAsync().Result);
    }

    private async Task<List<Optionator>> LoadOptionatorsAsync()
    {
        var tasks = _optionatorRepositories.Select(or => or.GetOptionatorsAsync());
        var results = await Task.WhenAll(tasks);
        var allOptionators = results.SelectMany(r => r.Optionators).ToList();
        return allOptionators;
    }

    public List<Optionator> GetOptionators()
    {
        return _optionatorsLazy.Value;
    }

    public Optionator RandomOptionator()
    {
        Random r = new Random();
        int next = r.Next(0, _optionatorsLazy.Value.Count);
        return _optionatorsLazy.Value[next];        
    }
}