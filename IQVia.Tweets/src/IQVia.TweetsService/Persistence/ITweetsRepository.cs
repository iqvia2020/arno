using IQvia.TweetsService.Models;
using System.Collections.Generic;

namespace IQvia.TweetsService.Persistence
{
    public interface ITweetsRepository
    {
        IEnumerable<Tweet> List();
    }
}
