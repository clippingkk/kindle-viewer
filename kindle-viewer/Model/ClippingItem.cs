using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kindle_viewer.Model
{
    public class ClippingItem
    {
        public string title { get; set; }
        public DateTime createdAt { get; set; }
        public string content { get; set; }
        public string location { get; set; }
        public string author { get; set; }

        public string toString() => $"{title}\n{content}\n{location}\n{author}\n{createdAt.ToString()}";
    }
}
