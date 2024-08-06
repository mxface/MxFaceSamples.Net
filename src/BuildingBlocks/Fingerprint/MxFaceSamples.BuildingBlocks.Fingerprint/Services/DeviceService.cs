using Microsoft.Extensions.Logging;
using MorFin_Auth;
using MxFaceSamples.BuildingBlocks.Fingerprint.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace MxFaceSamples.BuildingBlocks.Fingerprint.Services;

public class DeviceService(HttpClient httpClient, ILogger<DeviceService> logger) : IDeviceService
{
    private readonly string remoteServiceBaseUrl = "morfinauth/";

    public async Task<int> GetConnectedDevices(List<string> devices)
    {
        var response = (await this.PostRequestAsync("connecteddevicelist")).FirstOrDefault();

        if (IsSuccessStatusCode(response.Key))
        {
            //TODO: Check if the response is a list of strings and fix the deserialization

            devices.AddRange(JsonSerializer.Deserialize<List<string>>(response.Value));

            return devices.Count;
        }
        else return 0;
    }

    public async Task<int> GetSupportedDevices(DEVICE_LIST[] deviceList)
    {
        var response = (await this.PostRequestAsync("supporteddevicelist")).FirstOrDefault();

        if (IsSuccessStatusCode(response.Key))
        {
            //TODO: Check if the response is a list of strings and fix the deserialization

            return 0;
        }

        else return -1;
    }

    public async Task<int> Init(string productName)
    {
        var response = (await this.PostRequestAsync("initdevice", new { ConnectedDvc = productName })).FirstOrDefault();

        if (IsSuccessStatusCode(response.Key))
        {
            //TODO: Check if the response is a list of strings and fix the deserialization

            return 0;
        }
        else return -1;

    }

    public async Task<int> IsDeviceConnected(string productName)
    {
        var response = (await this.PostRequestAsync("checkdevice", new { ConnectedDvc = productName })).FirstOrDefault();

        if (IsSuccessStatusCode(response.Key))
        {
            //TODO: Check if the response is a list of strings and fix the deserialization

            return 0;
        }
        else return -1;
    }

    public async Task UnInit()
    {
        var response = (await this.PostRequestAsync("uninitdevice")).FirstOrDefault();

        if (IsSuccessStatusCode(response.Key))
        {
            //TODO: Check if the response is a list of strings and fix the deserialization
        }
    }

    private async Task<Dictionary<int, string>> PostRequestAsync(string endpoint, object content = null)
    {
        endpoint = Path.Combine(remoteServiceBaseUrl, endpoint);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);

        if (content != null)
            requestMessage.Content = JsonContent.Create(content);

        var response = await httpClient.SendAsync(requestMessage);

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
        else return new Dictionary<int, string> { { 0, String.Empty } };
    }

    private bool IsSuccessStatusCode(int statusCode)
    {
        return ((int)statusCode >= 200) && ((int)statusCode <= 299);
    }
}
