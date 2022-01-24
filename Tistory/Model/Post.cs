using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tistory.Model
{
    public class Post
    {
        public string id { get; set; }
        public string title { get; set; }
        public string postUrl { get; set; }
        public string visibility { get; set; }
        public string categoryId { get; set; }
        public string comments { get; set; }
        public string trackbacks { get; set; }
        public string date { get; set; }
    }
}
