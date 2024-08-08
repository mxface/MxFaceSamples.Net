//using MorFin_Auth;
using MxFaceSamples.BuildingBlocks.Fingerprint.Models;
using System.Reflection;

namespace MxFaceSamples.BuildingBlocks.Fingerprint.Interfaces;

public interface ICaptureService
{
    /// <summary>
    /// Starts the fingerprint capture process.
    /// Requires sucessful device initialization.
    /// </summary>
    /// <param name="Timeout">Timeout value set in milliseconds. If set to 0 then capture will be stop after finger detected with desired quality</param>
    /// <param name="MinimumQuality">Quality range 1 to 100</param>
    /// <returns>
    /// 0 = Capture started
    /// 0! = Capture start failed
    /// </returns>
    Task<CaptureViewModel> StartCaptureAsync(int Timeout = 10, int MinimumQuality = 60);

    /// <summary>
    /// Forcefully stops the capture process
    /// </summary>
    /// <returns>
    /// 0 = Capture stopped
    /// 0! = Capture stop failed
    /// </returns>
    Task<int> StopCaptureAsync();

    /// <summary>
    /// Get the fingerprint image.
    /// Requires sucessful fingerprint capture.
    /// </summary>
    /// <param name="image"></param>
    /// <param name="Format"><see cref="MorFin_Auth.IMAGE_FORMAT"></param>
    /// <param name="CompressionRatio"></param>
    /// <returns>
    /// 0 = Success
    /// 0! = Failed
    /// </returns>
    //Task<int> GetFringerprintImageAsync(out byte[] image, IMAGE_FORMAT Format, int CompressionRatio);

}
