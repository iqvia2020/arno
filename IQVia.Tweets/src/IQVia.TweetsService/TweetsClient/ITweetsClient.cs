using IQvia.TweetsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQvia.TweetsService.TweetsClient
{
    public interface ITweetsClient
    {
        Task<IEnumerable<Tweet>> List();
        void Add(Tweet tweet);
    }
}
