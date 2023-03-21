
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using optionator.core;

namespace optionator.data;

public class OptionatorGitHubRepositoryClient
{
    private readonly HttpClient _httpClient;

    public OptionatorGitHubRepositoryClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<OptionatorGitHubRepositoryResponse> GetOptionatorsAsync(OptionatorGitHubRepositoryConfig optionatorGitHubRepositoryConfig)
    {
        OptionatorGitHubRepositoryResponse optionatorRepositoryResponse
            = new OptionatorGitHubRepositoryResponse();

        try
        {
            var response = await _httpClient.GetAsync(optionatorGitHubRepositoryConfig.Url);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var optionators = JsonSerializer.Deserialize<List<Optionator>>(jsonContent);
            if (optionators is not null)
            {
                optionatorRepositoryResponse.Optionators = optionators;
            }
        }
        catch (HttpRequestException ex)
        {
            optionatorRepositoryResponse.Message = ex.Message;
        }
        catch (JsonException ex)
        {
            optionatorRepositoryResponse.Message = ex.Message;
        }
        catch (Exception ex)
        {
            optionatorRepositoryResponse.Message = ex.Message;
        }

        return optionatorRepositoryResponse;
    }
}