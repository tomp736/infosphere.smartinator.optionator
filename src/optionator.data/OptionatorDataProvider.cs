using optionator.core;

namespace optionator.data;

public class OptionatorDataProvider
{
    private readonly OptionatorGitHubRepositoryClient _optionatorGitHubRepositoryClient;
    private readonly List<OptionatorGitHubRepositoryConfig> _optionatorGitHubRepositoryConfig;
    private readonly Lazy<List<Optionator>> _optionatorsLazy;

    public OptionatorDataProvider(OptionatorGitHubRepositoryClient optionatorGitHubRepositoryClient, List<OptionatorGitHubRepositoryConfig> optionatorGitHubRepositoryConfig)
    {
        _optionatorGitHubRepositoryClient = optionatorGitHubRepositoryClient;
        _optionatorGitHubRepositoryConfig = optionatorGitHubRepositoryConfig;
        _optionatorsLazy = new Lazy<List<Optionator>>(() => LoadOptionatorsAsync().Result);
    }

    private async Task<List<Optionator>> LoadOptionatorsAsync()
    {
        var tasks = _optionatorGitHubRepositoryConfig.Select(config => _optionatorGitHubRepositoryClient.GetOptionatorsAsync(config));
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