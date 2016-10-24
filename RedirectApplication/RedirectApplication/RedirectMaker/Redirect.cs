using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedirectApplication.RedirectDB;
using RedirectApplication.Models;
using Newtonsoft.Json;
using System.IO;

namespace RedirectApplication.RedirectMaker
{
    public class Redirect
    {
        RedirectRepository db = new RedirectRepository();
        RedirectRule rule = new RedirectRule();

        public DbJson DeserializationJsonFromDb(RedirectRule rule)
        {
            var json = db.GetJsonFromDb(rule.TargetUrl);
            var reader = new JsonTextReader(new StringReader(json));
            return JsonSerializer.CreateDefault().Deserialize<DbJson>(reader);
        }
    }
}
