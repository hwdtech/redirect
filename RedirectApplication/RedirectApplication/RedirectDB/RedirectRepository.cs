using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using RedirectApplication.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace RedirectApplication.RedirectDB
{
    public class RedirectRepository
    {
        private RedirectContext context;
        public RedirectRepository()
        {
            context = new RedirectContext();
        }
        public Boolean IsRuleExist(String Url)
        {
            return context.RedirectRules.Any(x => x.TargetUrl == Url);
        }
    }
}