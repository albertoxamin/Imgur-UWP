using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace imgur.ImgurAPI
{
    public class ImgurClient
    {
        private string _clientID;
        private string _clientSecret;

        public ImgurClient(string clientID, string clientSecret)
        {
            _clientID = clientID;
            _clientSecret = clientSecret;
        }

        public async void GetAccessToken(Action<string> onCompletion)
        {
            HttpClient client = new HttpClient();
            string s = await client.GetStringAsync(new Uri(string.Format("https://api.imgur.com/oauth2/authorize?client_id={0}&redirect_uri={1}&response_type=code&state=CODE_RECEIVED",
                _clientID, "")));
                    onCompletion(s);
        }

        /// <summary>
        /// Get the images from the main gallery.
        /// This call DOES NOT require authentcation.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        public async System.Threading.Tasks.Task<ImgurImageData> GetMainGalleryImages(ImgurGallerySection section, ImgurGallerySort sort, int page)
        {
            string _sort = sort.ToString().ToLower();
            string _section = section.ToString().ToLower();

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(string.Format(ImgurEndpoints.MainGallery, _section, _sort, page)),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Authorization", "Client-ID " + _clientID);
            ImgurAPI.ImgurImageData iid = null;
            var task = await client.SendAsync(request).ContinueWith(
                async (s) =>
                {
                    try
                    {
                        string Result = await s.Result.Content.ReadAsStringAsync();
                        var imageData = JsonConvert.DeserializeObject<ImgurImageData>(Result);
                        iid = imageData;
                    }
                    catch
                    {
                        Debug.WriteLine("[SERVICE] Imgur seems to be inaccessible.");
                    }
                });
            return iid;
        }
        public async System.Threading.Tasks.Task<List<AlbumImage>> GetAlbumImages(string id)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(string.Format(ImgurEndpoints.AlbumImages, id)),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Authorization", "Client-ID " + _clientID);

            List<AlbumImage> iid = null;
            var task = await client.SendAsync(request).ContinueWith(
                async (s) =>
                {
                    try
                    {
                        string Result = await s.Result.Content.ReadAsStringAsync();
                        var imageData = JsonConvert.DeserializeObject<AlbumImages>(Result);
                        iid = imageData.data;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Can't get album images. Album ID = " + id + "\n" + e);
                    }
                });
            return iid;
        }

        public async System.Threading.Tasks.Task<List<Topic>> GetTopics()
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(string.Format(ImgurEndpoints.Topics)),
                Method = HttpMethod.Get,
            };
            //request.Headers.Add("Authorization", "Client-ID " + _clientID);

            List<Topic> iid = null;
            var task = await client.SendAsync(request).ContinueWith(
                async (s) =>
                {
                    try
                    {
                        string Result = await s.Result.Content.ReadAsStringAsync();
                        var imageData = JsonConvert.DeserializeObject<TopicsData>(Result);
                        iid = imageData.data;
                    }
                    catch (Exception e)
                    {
                    }
                });
            return iid;
        }

        public async System.Threading.Tasks.Task<ImgurAccount.Account> GetAccount(string username)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(string.Format(ImgurEndpoints.AccountBase, username)),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Authorization", "Client-ID " + _clientID);
            ImgurAPI.ImgurAccount.Account ia = null;
            var task = await client.SendAsync(request).ContinueWith(
                async (s) =>
                {
                    try
                    {
                        string Result = await s.Result.Content.ReadAsStringAsync();
                        var imageData = JsonConvert.DeserializeObject<ImgurAccount.AccountBio>(Result);
                        ia = imageData.data;
                    }
                    catch
                    {
                        Debug.WriteLine("Couldn't get Account.");
                    }
                });
            return ia;
        }
        public async System.Threading.Tasks.Task<List<Comment>> GetAccountComments(string username)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(string.Format(ImgurEndpoints.UserComments, username)),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Authorization", "Client-ID " + _clientID);
            List<Comment> ia = null;
            var task = await client.SendAsync(request).ContinueWith(
                async (s) =>
                {
                    try
                    {
                        string Result = await s.Result.Content.ReadAsStringAsync();
                        var imageData = JsonConvert.DeserializeObject<ImgurAccount.UserComments>(Result);
                        ia = imageData.data;
                    }
                    catch
                    {
                        Debug.WriteLine("Couldn't get Account Comments.");
                    }
                });
            return ia;
        }
        public async System.Threading.Tasks.Task<ImgurAccount.AccountAwards> GetGalleryProfile(string username)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(string.Format(ImgurEndpoints.GalleryProfile, username)),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Authorization", "Client-ID " + _clientID);
            ImgurAPI.ImgurAccount.AccountAwards ia = null;
            var task = await client.SendAsync(request).ContinueWith(
                async (s) =>
                {
                    try
                    {
                        string Result = await s.Result.Content.ReadAsStringAsync();
                        var imageData = JsonConvert.DeserializeObject<ImgurAccount.GalleryProfile>(Result);
                        ia = imageData.data;
                    }
                    catch
                    {
                        Debug.WriteLine("[SERVICE] Imgur seems to be inaccessible.");
                    }
                });
            return ia;
        }

        public async System.Threading.Tasks.Task<List<Comment>> GetComments(string postType, string id)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(string.Format(ImgurEndpoints.PostComments,postType,id)),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Authorization", "Client-ID " + _clientID);
            List<Comment> iid = null;
            var task = await client.SendAsync(request).ContinueWith(
                async (s) =>
                {
                    try
                    {
                        string Result = await s.Result.Content.ReadAsStringAsync();
                        var imageData = JsonConvert.DeserializeObject<PostComments>(Result);
                        iid = imageData.data;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("[SERVICE] Imgur seems to be inaccessible." + e);
                    }
                });
            return iid;
        }

        public void UploadImage(byte[] content, string title, string description, Action<bool, ImgurAtomicImageData> onCompletion = null)
        {
            string BOUNDARY = Guid.NewGuid().ToString();
            string HEADER = string.Format("--{0}", BOUNDARY);
            string FOOTER = string.Format("--{0}--", BOUNDARY);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.imgur.com/3/upload");
            request.Method = "POST";
            request.Headers["Authorization"] = "Client-ID " + _clientID;
            request.ContentType = "multipart/form-data, boundary=" + BOUNDARY;

            StringBuilder builder = new StringBuilder();
            string base64string = Convert.ToBase64String(content);

            builder.AppendLine(HEADER);
            builder.AppendLine("Content-Disposition: form-data; name=\"image\"");
            builder.AppendLine();
            builder.AppendLine(base64string);

            if (!string.IsNullOrWhiteSpace(title))
            {
                builder.AppendLine(HEADER);
                builder.AppendLine("Content-Disposition: form-data; name=\"title\"");
                builder.AppendLine();
                builder.AppendLine(title);
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                builder.AppendLine(HEADER);
                builder.AppendLine("Content-Disposition: form-data; name=\"description\"");
                builder.AppendLine();
                builder.AppendLine(description);
            }

            builder.AppendLine(FOOTER);

            byte[] bData = Encoding.UTF8.GetBytes(builder.ToString());

            request.BeginGetRequestStream((result) =>
                {
                    using (Stream s = request.EndGetRequestStream(result))
                    {
                        s.Write(bData, 0, bData.Length);
                    }

                    request.BeginGetResponse((respResult) =>
                        {
                            try
                            {
                                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(respResult);
                                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                                {
                                    string jsonContent = reader.ReadToEnd();
                                    var imageData = JsonConvert.DeserializeObject<ImgurAtomicImageData>(jsonContent);

                                    Debug.WriteLine(jsonContent);

                                    if (onCompletion != null)
                                        onCompletion(true, imageData);
                                }
                            }
                            catch (WebException ex)
                            {
                                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                                {
                                    Debug.WriteLine(reader.ReadToEnd());

                                    if (onCompletion != null)
                                        onCompletion(false, null);
                                }
                            }
                        }, null);
                }, null);

        }
    }


    public static class ImgurEndpoints
    {
        public const string BaseUrl = "https://api.imgur.com/3";

        public const string MainGallery     = BaseUrl + "/gallery/{0}/{1}/{2}.json";
        public const string AlbumImages     = BaseUrl + "/album/{0}/images";
        public const string AccountBase     = BaseUrl + "/account/{0}";
        public const string GalleryProfile  = BaseUrl + "/account/{0}/gallery_profile";
        public const string UserComments    = BaseUrl + "/account/{0}/comments";
        public const string PostComments    = BaseUrl + "/{0}/{1}/comments";
        public const string Topics          = BaseUrl + "/topics/defaults";

    }

    public enum ImgurGallerySort
    {
        Viral,
        Time
    }

    public enum ImgurGallerySection
    {
        Hot,
        Top,
        User
    }
}