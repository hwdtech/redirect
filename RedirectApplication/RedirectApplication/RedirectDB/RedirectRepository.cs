using System;
using System.Linq;

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