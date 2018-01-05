﻿namespace RedditCringe.RedditApi
{
    public class Post
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Score { get; set; }

        public string Thumbnail { get; set; }

        public string Url { get; set; }

        public int NumberOfComments { get; set; }

    }
}
