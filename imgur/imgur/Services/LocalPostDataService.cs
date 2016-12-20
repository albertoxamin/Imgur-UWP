using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Imgur;
using Imgur.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace imgur.Services
{
    public class LocalPostDataService : IPostsDataService
    {
        private List<ImgurImage> _posts;

        public async Task<List<ImgurImage>> GetPosts(ImgurGallerySort sort)
        {
            if (_posts == null)
            {
                var data = await App.ServiceClient.GetMainGalleryImages(ImgurGallerySection.Hot, sort, 0);
                _posts = data.Images;
            }
            return _posts;
        }
    }
}
