using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tistory
{
    public class BlogInfo
    {
        public string name { get; set; }
        public string url { get; set; }
        public string secondaryUrl { get; set; }
        public string nickname { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string _default { get; set; }
        public string blogIconUrl { get; set; }
        public string faviconUrl { get; set; }
        public string profileThumbnailImageUrl { get; set; }
        public string profileImageUrl { get; set; }
        public string role { get; set; }
        public string blogId { get; set; }
        public string isEmpty { get; set; }
        public class Statistics
        {
            public string post { get; set; }
            public string comment { get; set; }
            public string trackback { get; set; }
            public string guestbook { get; set; }
        }

        public Statistics statistics { get; set; }
    }

}
