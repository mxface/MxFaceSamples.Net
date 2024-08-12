using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxFaceSamples.BuildingBlocks.Iris.Models
{
    public class CaptureViewModel
    {
        public string BitmapData { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public int Quality { get; set; }
        public string WSQInfo { get; set; }
    }
}
