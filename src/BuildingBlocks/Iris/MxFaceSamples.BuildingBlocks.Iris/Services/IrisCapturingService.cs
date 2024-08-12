using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MxFaceSamples.BuildingBlocks.Iris.Interfaces;
using MxFaceSamples.BuildingBlocks.Iris.Models;

namespace MxFaceSamples.BuildingBlocks.Iris.Services
{
    public class IrisCapturingService(HttpClient httpClient) : ICaptureService
    {
        private readonly string remoteServiceBaseUrl = "marvisauth/";

        public async Task<CaptureViewModel> StartCaptureAsync(int Timeout = 10)
        {
            var response = (await this.PostRequestAsync("capture", new {TimeOut = Timeout })).FirstOrDefault();

            if (IsSuccessStatusCode(response.Key))
            {
                return JsonSerializer.Deserialize<CaptureViewModel>(response.Value);
            }
            else return null;
        }

        public Task<int> StopCaptureAsync()
        {
            throw new NotImplementedException();
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
