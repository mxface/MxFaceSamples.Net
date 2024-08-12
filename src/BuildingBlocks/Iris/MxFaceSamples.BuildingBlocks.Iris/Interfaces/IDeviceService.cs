using MxFaceSamples.BuildingBlocks.Iris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxFaceSamples.BuildingBlocks.Iris.Interfaces
{
    public interface IDeviceService
    {
        /// <summary>
        /// Gets the Information of Connected Device 
        /// </summary>
        /// <returns></returns>
        Task<Device> GetDeviceInfoAsync();
    }
}
