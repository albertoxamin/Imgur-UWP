using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imgur.Models
{
    public class ImgurAccount
    {
        public class Account
        {
            public int id { get; set; }
            public string url { get; set; }
            public string bio { get; set; }
            public int reputation { get; set; }
            public int created { get; set; }
            public bool pro_expiration { get; set; }
            public string first_capital { get { return url[0].ToString().ToUpper(); } }
            public string reputation_resume
            {
                get
                {
                    return ImgurAccount.NotorietyFromPoints(reputation) + " • " + string.Format("{0:#,###0.#}", reputation)+ " Points";
                }
            }
            public string created_at
            {
                get
                {
                    return "Created " +(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(created)).ToString("MMM dd, yyyy");
                }
            }
        }
        public class UserComments
        {
            public List<Comment> data { get; set; }
            public bool success { get; set; }
            public int status { get; set; }
        }
        public class AccountBio
        {
            public Account data { get; set; }
            public bool success { get; set; }
            public int status { get; set; }
        }
        public class GalleryProfile
        {
            public AccountAwards data { get; set; }
            public bool success { get; set; }
            public int status { get; set; }
        }
        public class Trophy
        {
            public int id { get; set; }
            public string name { get; set; }
            public string name_clean { get; set; }
            public string description { get; set; }
            public object data { get; set; }
            public object data_link { get; set; }
            public int datetime { get; set; }
            public string image { get; set; }
        }
        public class AccountAwards
        {
            public int total_gallery_comments { get; set; }
            public int total_gallery_likes { get; set; }
            public int total_gallery_submissions { get; set; }
            public List<Trophy> trophies { get; set; }
        }

        public static string NotorietyFromPoints(int points)
        {
            if (points < 0)
                return "Forever Alone";
            else if (points < 400)
                return "Neutral";
            else if (points < 1000)
                return "Accepted";
            else if (points < 2000)
                return "Liked";
            else if (points < 4000)
                return "Trusted";
            else if (points < 8000)
                return "Idolized";
            else if (points < 20000)
                return "Renowed";
            else if (points >= 20000)
                return "Glorious";
            return "";
        }
    }
}
