using MxFaceSamples.BuildingBlocks.Fingerprint.Interfaces;
using MxFaceSamples.BuildingBlocks.Fingerprint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MxFaceSamples.BuildingBlocks.Fingerprint.Services
{
    public class FingerprintMatchingService(HttpClient httpClient): IMatchingService
    {
        private readonly string remoteServiceBaseUrl = "Fingerprint/";


       public async Task<EnrollmentResponse> EnrollAsync(EnrollmentRequest enroll)
        {
            var response = (await this.PostRequestAsync("EnrollmentRequest", new { fingerPrint = enroll.FingerPrint, externalId = enroll.ExternalId, group = enroll.Group })).FirstOrDefault();

            if (IsSuccessStatusCode(response.Key))
            {
                return JsonSerializer.Deserialize<EnrollmentResponse>(response.Value);
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
