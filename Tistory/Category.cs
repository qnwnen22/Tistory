using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tistory
{
    public class Category
    {
        public class Tistory
        {
            public string status { get; set; }
            public class Item
            {
                public string url { get; set; }
                public string secondaryUrl { get; set; }
            }

            public Item item { get; set; }
        }

        public Tistory tistory { get; set; }
    }
}
