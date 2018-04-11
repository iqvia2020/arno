using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IQVia.TweetsWeb.Models
{
    public class Tweet
    {
        public string Id { get; set; }
        public string Stamp { get; set; }
        public string Text { get; set; }

        public Tweet()
        {
        }

        public Tweet(string id) : this()
        {
            this.Id = id;
        }

        public Tweet(string id, string stamp) : this(id)
        {
            this.Stamp = stamp;
        }

        public Tweet(string id, string stamp, string text) : this(id, stamp)
        {
            this.Text = text;
        }
        public override string ToString()
        {
            return this.Id;
        }
    }
}
