using Imgur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgur.Services
{
    public interface IPostsDataService
    {
        Task<List<ImgurImage>> GetPosts(ImgurGallerySort sort);
    }
}
