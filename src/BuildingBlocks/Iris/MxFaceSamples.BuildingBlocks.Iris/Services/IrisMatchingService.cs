using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MxFaceSamples.BuildingBlocks.Iris.Interfaces;
using MxFaceSamples.BuildingBlocks.Iris.Models;

namespace MxFaceSamples.BuildingBlocks.Iris.Services
{
    public class IrisMatchingService(HttpClient httpClient) : IMatchingService
    {
        private readonly string remoteServiceBaseUrl = "api/Iris/";

        public async Task<EnrollmentResponse> EnrollAsync(EnrollmentRequest enroll)
        {
            var response = (await this.PostRequestAsync("Enroll", new { iris = enroll.Iris, externalId = enroll.ExternalId, group = enroll.Group })).FirstOrDefault();

            if (IsSuccessStatusCode(response.Key))
            {
                return JsonSerializer.Deserialize<EnrollmentResponse>(response.Value);
            }
            else return null;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest search)
        {
            var response = (await this.PostRequestAsync("Search", new { iris = search.Iris, group = search.Group })).FirstOrDefault();

            if (IsSuccessStatusCode(response.Key))
            {
                var searchResponse = JsonSerializer.Deserialize<SearchResponse>(response.Value);
                if (searchResponse.MatchResult != null)
                {
                    return searchResponse;
                }
                else
                {
                    searchResponse = new SearchResponse();
                    searchResponse.MatchResult = JsonSerializer.Deserialize<List<MatchResult>>(response.Value);
                }
                return searchResponse;
            }
            else return null;
        }

        public async Task<MatchResponse> MatchAsync(MatchRequest match)
        {
            var response = (await this.PostRequestAsync("Verify", new { iris1 = match.Iris1, iris2 = match.Iris2 })).FirstOrDefault();

            if (IsSuccessStatusCode(response.Key))
            {
                return JsonSerializer.Deserialize<MatchResponse>(response.Value);
            }
            else return null;
        }

        private async Task<Dictionary<int, string>> PostRequestAsync(string endpoint, object content = null)
        {
            endpoint = Path.Combine(remoteServiceBaseUrl, endpoint);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);

            if (content != null)
                requestMessage.Content = new StringContent(JsonSerializer.Serialize(content), null, "application/json");

            var response = await httpClient.SendAsync(requestMessage);

            //response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {

                return new Dictionary<int, string>
            {
                {
                    (int)response.StatusCode,
                    await response.Content.ReadAsStringAsync()
                }
            };
            }
            else return new Dictionary<int, string> { { 0, string.Empty } };
        }
        private bool IsSuccessStatusCode(int statusCode)
        {
            return ((int)statusCode >= 200) && ((int)statusCode <= 299);
        }


    }
}
