//using MorFin_Auth;

using MxFaceSamples.BuildingBlocks.Fingerprint.Models;

namespace MxFaceSamples.BuildingBlocks.Fingerprint.Interfaces;

public interface IDeviceService
{
    /// <summary>
    /// Initializes the connected fingerprint device
    /// </summary>
    /// <param name="productName">Connected device name</param>
    /// <returns>
    /// 0 = Initialization success
    /// 0! = Initialization failed
    /// </returns>
    Task<int> Init(string productName);

    /// <summary>
    /// Checks if the device is connected
    /// </summary>
    /// <param name="productName">Connected device name</param>
    /// <returns>
    /// 0 = Connected
    /// 0! = Not connected
    /// </returns>
    Task<int> IsDeviceConnected(string productName);

    /// <summary>
    /// Gets the connected devices 
    /// </summary>
    /// <param name="devices"></param>
    /// <returns>
    /// 
    /// </returns>
    Task<int> GetConnectedDevices(List<string> devices);

    /// <summary>
    /// Get the supported devices
    /// </summary>
    /// <param name="deviceList"></param>
    /// <param name="deviceCount"></param>
    /// <returns></returns>
    Task<int> GetSupportedDevices(List<string> deviceList);

    /// <summary>
    /// Uninitializes the connected fingerprint device
    /// </summary>
    /// <returns></returns>
    Task UnInit();

    /// <summary>
    /// Gets the Information of Connected Device 
    /// </summary>
    /// <returns></returns>
    Task<Device> GetDeviceInfoAsync(string deviceName);

}
