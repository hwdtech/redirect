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
        [Key]
        public string TargetUrl { get; set; }
        public string Conditions { get; set; }
    }
}
