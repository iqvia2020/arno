using IQVia.TweetsWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQVia.TweetsWeb.TweetsClient
{
    public class MemoryTweetsClient : ITweetsClient
    {
        public List<Tweet> Tweets { get; set; }
        public MemoryTweetsClient()
        {
            this.Tweets = new List<Tweet>();
        }

        public async Task<IEnumerable<Tweet>> List(DateTime startDate, DateTime endDate)
        {
            return await Task.Run(() =>
            {
                return Tweets;
            });
        }
    }
}
