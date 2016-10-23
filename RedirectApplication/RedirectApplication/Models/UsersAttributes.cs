using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedirectApplication.Models
{
    public class UsersAttributes
    {
        public string url { get; set; }
        public string browser { get; set; }
        public string OS { get; set; }
        public string MobileOrNot { get; set; }
        public string userIP { get; set; }
        public string language { get; set; }
        public string Country { get; set; }
        public string Time { get; set; }
    }
}