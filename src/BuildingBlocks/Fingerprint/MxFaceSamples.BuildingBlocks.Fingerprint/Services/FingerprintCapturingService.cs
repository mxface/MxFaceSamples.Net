using MxFaceSamples.BuildingBlocks.Fingerprint.Interfaces;

namespace MxFaceSamples.UI.Fingerprint.Services;

public class FingerprintCapturingService(HttpClient httpClient) : ICaptureService
{
    public Task<int> GetFingerprintTemplateAsync(out byte[] template, global::MorFin_Auth.TEMPLATE_FORMAT Format, int CompressionRatio)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetFringerprintImageAsync(out byte[] image, global::MorFin_Auth.IMAGE_FORMAT Format, int CompressionRatio)
    {
        throw new NotImplementedException();
    }

    public Task<int> StartAutoCaptureAsync(out int Qlt, out int Nfiq, int TimeOut = 10000, int MinimumQuality = 40)
    {
        throw new NotImplementedException();
    }

    public Task<int> StartCaptureAsync(int Timeout = 10000, int MinimumQuality = 40)
    {
        throw new NotImplementedException();
    }

    public Task<int> StopCaptureAsync()
    {
        throw new NotImplementedException();
    }
}
