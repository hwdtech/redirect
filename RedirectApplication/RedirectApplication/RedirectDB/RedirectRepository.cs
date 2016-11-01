using System;
using System.Linq;
using RedirectApplication.Models;

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

        public string GetJsonFromDb(string Url)
        {
            return context.RedirectRules.FirstOrDefault(x => x.TargetUrl == Url).Conditions;
        }

        public void AddRule(RedirectRule NewRule)
        {
            try
            {
                context.RedirectRules.Add(NewRule);
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("This TargetUrl already exist");
            }
        }
    }
}
