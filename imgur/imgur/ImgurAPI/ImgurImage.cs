using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;

namespace imgur.ImgurAPI
{
    public class ImgurGalleryData
    {
        public ImgurImageData data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
    }
    public class AlbumImages
    {
        public List<AlbumImage> data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
    }

    public class AlbumImage
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Visibility descriptionVisibility
        {
            get
            {
                if (description == null)
                    return Visibility.Collapsed;
                if (description.Trim() != "")
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public Visibility titleVisibility
        {
            get
            {
                if (title == null)
                    return Visibility.Collapsed;
                if (title.Trim() != "")
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public int datetime { get; set; }
        public string type { get; set; }
        public bool animated { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public int views { get; set; }
        public object bandwidth { get; set; }
        public object vote { get; set; }
        public bool favorite { get; set; }
        public object nsfw { get; set; }
        public object section { get; set; }
        public object account_url { get; set; }
        public object account_id { get; set; }
        public object comment_preview { get; set; }
        public string gifv { get; set; }
        public string webm { get; set; }
        public string mp4 { get; set; }
        public string link { get; set; }
        public bool looping { get; set; }
        public Visibility IsGif
        {
            get
            {
                if (animated)
                {
                    return Visibility.Visible;
                }
                else
                    return Visibility.Collapsed;
            }
        }
        public Visibility IsGifReverse
        {
            get
            {
                if (animated)
                {
                    return Visibility.Collapsed;
                }
                else
                    return Visibility.Visible;
            }
        }
    }

    public class ImgurImage //: ICollectionView
    {
        public MainPage mainPage;
        public Frame Frame;

        public event EventHandler<object> CurrentChanged;
        public event CurrentChangingEventHandler CurrentChanging;
        public event VectorChangedEventHandler<object> VectorChanged;
        public int RowSpan {
            get
            {
                int cal = Height / Width * 10;
                if (cal < 11)
                    cal = 11;
                if (cal > 25)
                    cal = 25;
                return cal;
            }
        }
        public int ColSpan = 1;
        public string ID { get; set; }
        [JsonProperty(PropertyName = "deletehash")]
        public string DeleteHash { get; set; }
        public string hash { get; set; }
        public string HashLink
        {
            get
            {
                return "http://i.imgur.com/" + hash + ".jpg";
            }
        }
        public string AlbumGifLink { get { return "http://i.imgur.com/" + hash + ".mp4"; } }
        public string Title { get; set; }
        public string description { get; set; }
        //public DateTime DateTime { get; set; }
        public string Type { get; set; }
        [JsonProperty(PropertyName = "animated")]
        public bool IsAnimated { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Int64 Size { get; set; }
        public Int64 Views { get; set; }
        [JsonProperty(PropertyName = "account_url")]
        public string AccountUrl { get; set; }
        public string cover { get; set; }
        public string link { get; set; }
        public byte GetConnectionGeneration()
        {
            try
            {
                ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();
                if (profile.IsWwanConnectionProfile)
                {
                    WwanDataClass connectionClass = profile.WwanConnectionProfileDetails.GetCurrentDataClass();
                    switch (connectionClass)
                    {
                        //2G-equivalent
                        case WwanDataClass.Edge:
                        case WwanDataClass.Gprs:
                        //3G-equivalent
                        case WwanDataClass.Cdma1xEvdo:
                        case WwanDataClass.Cdma1xEvdoRevA:
                        case WwanDataClass.Cdma1xEvdoRevB:
                        case WwanDataClass.Cdma1xEvdv:
                        case WwanDataClass.Cdma1xRtt:
                        case WwanDataClass.Cdma3xRtt:
                        case WwanDataClass.CdmaUmb:
                        case WwanDataClass.Umts:
                        case WwanDataClass.Hsdpa:
                        case WwanDataClass.Hsupa:
                        //4G-equivalent
                        case WwanDataClass.LteAdvanced:
                            return 0;

                        //not connected
                        case WwanDataClass.None:
                            return 2;

                        //unknown
                        case WwanDataClass.Custom:
                        default:
                            return 2;
                    }
                }
                else if (profile.IsWlanConnectionProfile)
                {
                    return 1;
                }
                return 2;
            }
            catch (Exception)
            {
                return 2; //as default
            }

        }
        public string Thumbnail
        {
            get
            {
                string quality = "m";
                if (GetConnectionGeneration() == 0)
                    quality = "t";
                if (!IsAlbum)
                    return "http://i.imgur.com/" + ID +quality+ ".jpg";
                else
                    return "http://i.imgur.com/" + cover +quality+ ".jpg";
            }
        }
        public Visibility IsGif
        {
            get
            {
                if (IsAnimated)
                {
                    return Visibility.Visible;
                }
                else
                    return Visibility.Collapsed;
            }
        }
        public Visibility IsGifReverse
        {
            get
            {
                if (IsAnimated)
                {
                    return Visibility.Collapsed;
                }
                else
                    return Visibility.Visible;
            }
        }
        public Visibility AlbumVisbility
        {
            get
            {
                if (IsAlbum)
                {
                    return Visibility.Visible;
                }
                else
                    return Visibility.Collapsed;
            }
        }
        public Visibility AlbumVisbilityReverse
        {
            get
            {
                if (IsAlbum)
                {
                    return Visibility.Collapsed;
                }
                else
                    return Visibility.Visible;
            }
        }
        public string Bandwidth { get; set; }
        public int Ups { get; set; }
        public string Upvotes { get { return Ups.ToString(); } }
        public string CommCount { get { return comment_count.ToString(); } }
        public int Downs { get; set; }
        public int Score { get; set; }
        [JsonProperty(PropertyName = "is_album")]
        public bool IsAlbum { get; set; }
        public bool looping { get; set; }
        public string vote { get; set; }
        public int datetime { get; set; }
        public string shortDate
        {
            get
            {
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(datetime);
                if (DateTime.Now - dt < TimeSpan.FromDays(1))
                    if (DateTime.Now - dt < TimeSpan.FromHours(1))
                        return (DateTime.Now - dt).Minutes.ToString() + " Minutes";
                    else
                        return (DateTime.Now - dt).Hours.ToString() + " Hours";
                else
                    return (DateTime.Now - dt).Days.ToString() + " Days";

            }
        }
        public int comment_count { get; set; }
        public List<Comment> comment_preview { get; set; }
        public int images_count { get; set; }
        public string mp4 { get; set; }
        public string gifv { get; set; }
        public bool favorite { get; set; }
        public bool nsfw { get; set; }
        public delegate void LoadHandler();
        public event LoadHandler LoadPost;
        public void Load()
        {
            if (LoadPost != null)
                LoadPost.Invoke();
        }
    }

    public class PostComments
{
    public List<Comment> data { get; set; }
    public bool success { get; set; }
    public int status { get; set; }
}
    public class Comment: IComparable<Comment>
    {
        public string id { get; set; }
        public string image_id { get; set; }
        public string image_link
        {
            get
            {
                return "http://i.imgur.com/"+ image_id+ "m.jpg";
            }
        }
        public string comment { get; set; }
        public MainPage mainPage;
        public string shortDate
        {
            get
            {
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(datetime);
                if (DateTime.Now - dt < TimeSpan.FromDays(1))
                    if (DateTime.Now - dt < TimeSpan.FromHours(1))
                        return (DateTime.Now - dt).Minutes.ToString() + "m";
                    else
                        return (DateTime.Now - dt).Hours.ToString() + "h";
                else
                    return (DateTime.Now - dt).Days.ToString() + "d";

            }
        }
        public string author { get; set; }
        public int author_id { get; set; }
        public bool on_album { get; set; }
        public string album_cover { get; set; }
        public int ups { get; set; }
        public int downs { get; set; }
        public int points { get; set; }
        public int datetime { get; set; }
        public int parent_id { get; set; }
        public bool deleted { get; set; }
        public object vote { get; set; }
        public string platform { get; set; }
        public List<Comment> children { get; set; }
        public int repliesCount {
            get
            {
                if (children != null)
                    return children.Count;
                else
                    return 0;
            }
        }
        public Visibility repliesVisibility
        {
            get
            {
                if (repliesCount == 0)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        public int CompareTo(Comment other)
        {
            if (points < other.points)
                return 1;
            else
                return -1;
        }
    }
}
