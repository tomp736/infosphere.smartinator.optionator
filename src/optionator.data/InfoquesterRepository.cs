
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using optionator.core;

namespace optionator.data;

public class InfoquesterRepository
{
    private readonly string _githubUser;
    private readonly string _repositoryName;
    private readonly string _branchName;
    private readonly string _filePath;
    private readonly HttpClient _httpClient;

    public InfoquesterRepository(string githubUser, string repositoryName, string branchName, string filePath)
    {
        _githubUser = githubUser;
        _repositoryName = repositoryName;
        _branchName = branchName;
        _filePath = filePath;
        _httpClient = new HttpClient();
    }

        public async Task<InfoquesterRepositoryResponse> GetInfoquestersAsync()
        {
            InfoquesterRepositoryResponse infoquesterRepositoryResponse 
                = new InfoquesterRepositoryResponse();

            var url = $"https://raw.githubusercontent.com/{_githubUser}/{_repositoryName}/{_branchName}/{_filePath}";
            try
            {
                var response = await _httpClient.GetAsync(url);
                var contentStream = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);
                var serializer = new JsonSerializer();
                var infoquesters = serializer.Deserialize<List<Infoquester>>(jsonReader);
                if(infoquesters is not null)
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