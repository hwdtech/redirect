using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RedirectApplication.Models
{
    public class RedirectRule
    {
        public string Id { get; set; }
        public string Data { get; set; }
    }
}