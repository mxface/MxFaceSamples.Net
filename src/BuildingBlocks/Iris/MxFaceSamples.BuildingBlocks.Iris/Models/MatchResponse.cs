using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MxFaceSamples.BuildingBlocks.Iris.Models
{
    public class MatchResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("matchingScore")]
        public int MatchingScore { get; set; }

        [JsonPropertyName("matched")]
        public int Matched { get; set; }
    }
}
