
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using optionator.core;

namespace optionator.data;

public class InfoquesterRepository
{
    private readonly string _githubUser;
    private readonly string _repositoryName;
    private readonly string _branchName;
    private readonly string _filePath;
    private readonly HttpClient _httpClient;

    public InfoquesterRepository(string githubUser, string repositoryName, string branchName, string filePath, HttpClient httpClient)
    {
        _githubUser = githubUser;
        _repositoryName = repositoryName;
        _branchName = branchName;
        _filePath = filePath;
        _httpClient = httpClient;
    }


    public async Task<InfoquesterRepositoryResponse> GetInfoquestersAsync()
    {
        InfoquesterRepositoryResponse infoquesterRepositoryResponse
            = new InfoquesterRepositoryResponse();

        var url = $"https://raw.githubusercontent.com/{_githubUser}/{_repositoryName}/{_branchName}/{_filePath}";
        try
        {
            var response = await _httpClient.GetAsync(url);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var infoquesters = JsonSerializer.Deserialize<List<Infoquester>>(jsonContent);
            if (infoquesters is not null)
            {
                infoquesterRepositoryResponse.Infoquesters = infoquesters;
            }
        }
        catch (HttpRequestException ex)
        {
            infoquesterRepositoryResponse.Message = ex.Message;
        }
        catch (JsonException ex)
        {
            infoquesterRepositoryResponse.Message = ex.Message;
        }
        catch (Exception ex)
        {
            infoquesterRepositoryResponse.Message = ex.Message;
        }

        return infoquesterRepositoryResponse;
    }
}