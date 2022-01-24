using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tistory
{
    public class PostInfo
    {
        public Tistory tistory { get; set; }

        public class Tistory
        {
            public string status { get; set; }
            public class Item
            {
                public string url { get; set; }
                public string secondaryUrl { get; set; }
                public string page { get; set; }
                public string count { get; set; }
                public string totalCount { get; set; }
                public List<Post> posts { get; set; }
            }

            public Item item { get; set; }
        }



    }
}
