using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxFaceSamples.BuildingBlocks.Fingerprint.Models
{
    public class EnrollmentRequest
    {
        public string? FingerPrint { get; set; }
        public string? ExternalId { get; set; } = Guid.NewGuid().ToString();
        public string? Group {  get; set; }
    }
}
