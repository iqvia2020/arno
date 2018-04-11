using IQVia.TweetsWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQVia.TweetsWeb.TweetsClient
{
    public interface ITweetsClient
    {
        Task<IEnumerable<Tweet>> List(DateTime startDate, DateTime endDate);
    }
}
