using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tistory
{
    public class BlogInformation
    {
        public class Tistory
        {
            public string status { get; set; }
            public class Item
            {
                public string id { get; set; }
                public string userId { get; set; }
                public List<BlogInfo> blogs { get; set; }
            }

            public Item item { get; set; }
        }

        public Tistory tistory { get; set; }



    }

}
