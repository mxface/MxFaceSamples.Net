using MxFaceSamples.BuildingBlocks.Fingerprint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxFaceSamples.BuildingBlocks.Fingerprint.Interfaces
{
    public interface IMatchingService
    {
        Task<EnrollmentResponse> EnrollAsync(EnrollmentRequest enroll);
    }
}
