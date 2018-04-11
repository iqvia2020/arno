using IQvia.TweetsService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQvia.TweetsService.TweetsClient
{
    public interface ITweetsClient
    {
        Task<IEnumerable<Tweet>> List(DateTime startDate, DateTime endDate);
        void Add(Tweet tweet);
    }
}
