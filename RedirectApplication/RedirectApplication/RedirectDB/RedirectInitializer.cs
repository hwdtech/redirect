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
                //          Browser                                 language                                Country                                     IP                                                  OS                              Device                                  Time
                //{rules: [{Name": "ByBrowser", "Browser":"Chrome"},{"Name":"ByLanguauge", "Language":"en"},{"Name":"ByCountry","Country":"Australia"},{"Name":"ByIP","IP":["172.168.0.128","172.168.1.168"]},{"Name":"ByOS","OS":"Windows"},{"Name":"ByDevice","Device":"Mobile"},{"Name":"ByDate","Date":">18.10.15"}], "Url":"https://vk.com/ouromsk"}
                //new RedirectRule {TargetUrl = "https://habrahabr.ru/post/175999/", Conditions = "[{Name\": \"ByBrowser\", \"Browser\":\"Chrome\"},{\"Name\":\"ByLanguauge\", \"Language\":\"en\"},{\"Name\":\"ByCountry\",\"Country\":\"Australia\"},{\"Name\":\"ByIP\",\"IP\":[\"172.168.0.128\",\"172.168.1.168\"]},{\"Name\":\"ByOS\",\"OS\":\"Windows\"},{\"Name\":\"ByDevice\",\"Device\":\"Mobile\"},{\"Name\":\"ByDate\",\"Date\":\">18.10.15\"}]" }
            };
            redirs.ForEach(s => context.RedirectRules.Add(s));
            context.SaveChanges();
        }
    }
}