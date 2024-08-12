using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxFaceSamples.BuildingBlocks.Iris.Models
{
    public class EnrollmentRequest
    {
        public string? Iris { get; set; }
        public string? ExternalId { get; set; } = Guid.NewGuid().ToString();
        public string? Group { get; set; }
    }
}
