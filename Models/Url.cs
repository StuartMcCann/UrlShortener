using System;
using System.Linq;

namespace UrlShorterner.Models
{
    public class Url
    {
        public int UrlID { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
