using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{
    public class Article
    {
        public int Serial { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public int AuthorID { get; set; }

        public virtual Author Author { get; set; }
    }
}
