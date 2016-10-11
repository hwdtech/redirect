using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
namespace RedirectApplication.RedirectDB
{
    public class RedirectConfiguration : DbConfiguration
    {
        public RedirectConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}