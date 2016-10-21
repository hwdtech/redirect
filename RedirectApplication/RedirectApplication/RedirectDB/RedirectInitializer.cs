using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RedirectApplication.Models;

namespace RedirectApplication.RedirectDB
{
    public class RedirectInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RedirectContext>
    {
        protected override void Seed(RedirectContext context)
        {
            var redirs = new List<RedirectRule>
            {
            };
            redirs.ForEach(s => context.RedirectRules.Add(s));
            context.SaveChanges();
        }
    }
}
