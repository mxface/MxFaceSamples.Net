using MxFaceSamples.BuildingBlocks.Iris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxFaceSamples.BuildingBlocks.Iris.Interfaces
{
    public interface IMatchingService
    {
        Task<EnrollmentResponse> EnrollAsync(EnrollmentRequest enroll);

        Task<SearchResponse> SearchAsync(SearchRequest search);

        Task<MatchResponse> MatchAsync(MatchRequest match);
    }
}
