using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedirectApplication.Models
{
    public class UsersAttributes
    {
        public string Url { get; set; }
        public string Browser { get; set; }
        public string OS { get; set; }
        public bool MobileOrNot { get; set; }
        public uint UserIP { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public DateTime Time { get; set; }
    }
}
