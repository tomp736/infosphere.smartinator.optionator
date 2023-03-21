
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using optionator.core;

namespace optionator.data;

public class OptionatorRepository
{
    private readonly string _githubUser;
    private readonly string _repositoryName;
    private readonly string _branchName;
    private readonly string _filePath;
    private readonly HttpClient _httpClient;

    public OptionatorRepository(string githubUser, string repositoryName, string branchName, string filePath, HttpClient httpClient)
    {
        _githubUser = githubUser;
        _repositoryName = repositoryName;
        _branchName = branchName;
        _filePath = filePath;
        _httpClient = httpClient;
    }

    public async Task<OptionatorRepositoryResponse> GetOptionatorsAsync()
    {
        OptionatorRepositoryResponse optionatorRepositoryResponse
            = new OptionatorRepositoryResponse();

        var url = $"https://raw.githubusercontent.com/{_githubUser}/{_repositoryName}/{_branchName}/{_filePath}";
        try
        {
            var response = await _httpClient.GetAsync(url);
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