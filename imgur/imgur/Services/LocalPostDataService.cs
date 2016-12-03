using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imgur.Models;
using Newtonsoft.Json;

namespace imgur.Services
{
    public class LocalPostDataService : IPostsDataService
    {
        private List<ImgurImage> _posts;
        public List<ImgurImage> GetPosts()
        {
            if (_posts == null)
                LoadPosts();
            return _posts;
        }

        private void LoadPosts()
        {
            ImgurImage img = JsonConvert.DeserializeObject<ImgurImage>("{\n    \"data\": {\n        \"id\": \"SbBGk\",\n        \"title\": null,\n        \"description\": null,\n        \"datetime\": 1341533193,\n        \"type\": \"image/jpeg\",\n        \"animated\": false,\n        \"width\": 2559,\n        \"height\": 1439,\n        \"size\": 521916,\n        \"views\": 1,\n        \"bandwidth\": 521916,\n        \"deletehash\": \"eYZd3NNJHsbreD1\"\n        \"section\": null,\n        \"link\": \"http://i.imgur.com/SbBGk.jpg\"\n    },\n    \"success\": true,\n    \"status\": 200\n}");
            _posts = new List<ImgurImage> {img};
        }
    }
}
