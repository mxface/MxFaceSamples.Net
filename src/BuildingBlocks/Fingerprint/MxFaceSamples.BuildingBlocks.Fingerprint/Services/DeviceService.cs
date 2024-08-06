using MorFin_Auth;
using MxFaceSamples.BuildingBlocks.Fingerprint.Interfaces;
using System.Text.Json;

namespace MxFaceSamples.BuildingBlocks.Fingerprint.Services;

public class DeviceService(HttpClient httpClient) : IDeviceService
{
    private readonly string remoteServiceBaseUrl = "morfinauth/";

    public async Task<int> GetConnectedDevices(List<string> devices)
    {
        var connectedDeviceEndpoint = Path.Combine(remoteServiceBaseUrl, "connecteddevicelist");

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, remoteServiceBaseUrl);

        var response = await httpClient.SendAsync(requestMessage);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            devices.AddRange(JsonSerializer.Deserialize<List<string>>(responseContent));

            return 0;
        }
        else return (int)response.StatusCode;
    }

    public Task<int> GetSupportedDevices(DEVICE_LIST[] deviceList, out int deviceCount)
    {
        var connectedDeviceEndpoint = Path.Combine(remoteServiceBaseUrl, "supporteddevicelist");

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, remoteServiceBaseUrl);

        var response = httpClient.SendAsync(requestMessage).Result;

        if (response.IsSuccessStatusCode)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;

            devices.AddRange(JsonSerializer.Deserialize<List<string>>(responseContent));

            return 0;
        }
        else return (int)response.StatusCode;
    }

    public Task<int> Init(string productName, ref FINGER_DEVICE_INFO deviceInfo)
    {
        throw new NotImplementedException();
    }

    public Task<int> IsDeviceConnected(string productName)
    {
        throw new NotImplementedException();
    }

    public Task UnInit()
    {
        throw new NotImplementedException();
    }
}
