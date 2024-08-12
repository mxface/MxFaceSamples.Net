using MxFaceSamples.BuildingBlocks.Iris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxFaceSamples.BuildingBlocks.Iris.Interfaces
{
    public interface ICaptureService
    {
        /// <summary>
        /// Starts the iris capture process.
        /// Requires sucessful device initialization.
        /// </summary>
        /// <param name="Timeout">Timeout value set in milliseconds. If set to 0 then capture will be stop after iris detected with desired quality</param>
        /// <param name="MinimumQuality">Quality range 1 to 100</param>
        /// <returns>
        /// 0 = Capture started
        /// 0! = Capture start failed
        /// </returns>
        Task<CaptureViewModel> StartCaptureAsync(int Timeout = 10);

        /// <summary>
        /// Forcefully stops the capture process
        /// </summary>
        /// <returns>
        /// 0 = Capture stopped
        /// 0! = Capture stop failed
        /// </returns>
        Task<int> StopCaptureAsync();
    }
}
