using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedirectApplication.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace RedirectApplication.RedirectDB
{
    public class RedirectContext : DbContext
    {
        public RedirectContext() : base("DefaultConnection")
        {

        }
        public DbSet<RedirectRule> RedirectRules { get; set; }
    }
}