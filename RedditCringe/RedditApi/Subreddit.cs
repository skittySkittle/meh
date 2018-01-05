using System.Collections.Generic;

namespace RedditCringe.RedditApi
{
    public class Subreddit
    {
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
    }
}
