using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;

namespace imgur.ImgurAPI
{
    public class ImgurImageData
    {
        [JsonProperty(PropertyName = "data")]
        public List<ImgurImage> Images { get; set; }
        public ImgurImage image { get; set; }
        public List<Comment> captions { get; set; }
    }

    public class TopPost
    {
        public string id { get; set; }
        public string thumbnail
        {
            get
            {
                return "http://i.imgur.com/" + id + "t.jpg";
            }
        }
        public string title { get; set; }
        public string description { get; set; }
        public int datetime { get; set; }
        public string type { get; set; }
        public bool animated { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public int views { get; set; }
        public int bandwidth { get; set; }
        public object vote { get; set; }
        public bool? favorite { get; set; }
        public bool nsfw { get; set; }
        public string section { get; set; }
        public string account_url { get; set; }
        public int account_id { get; set; }
        public object comment_preview { get; set; }
        public string topic { get; set; }
        public int topic_id { get; set; }
        public string gifv { get; set; }
        public string webm { get; set; }
        public string mp4 { get; set; }
        public string link { get; set; }
        public bool looping { get; set; }
        public int comment_count { get; set; }
        public int ups { get; set; }
        public int downs { get; set; }
        public int points { get; set; }
        public int score { get; set; }
        public bool is_album { get; set; }
        public string cover { get; set; }
        public int? cover_width { get; set; }
        public int? cover_height { get; set; }
        public string privacy { get; set; }
        public string layout { get; set; }
        public int? images_count { get; set; }
    }

    public class Topic
    {
        public int id { get; set; }
        public int index { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object css { get; set; }
        public bool ephemeral { get; set; }
        public TopPost topPost { get; set; }
        public Visibility selected { get; set; }
        public Topic()
        {

        }
        public Topic(string id, string n, string d)
        {
            name = n;
            description = d;
            topPost = new TopPost();
            topPost.id = id;
        }
    }

    public class TopicsData
    {
        public List<Topic> data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
    }
}
