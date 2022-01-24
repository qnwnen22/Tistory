using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tistory.Model
{
    public class PostResult
    {
        public class Tistory
        {
            public string status { get; set; }
            public string postId { get; set; }
            public string url { get; set; }
        }
        public Tistory tistory { get; set; }
    }
}
