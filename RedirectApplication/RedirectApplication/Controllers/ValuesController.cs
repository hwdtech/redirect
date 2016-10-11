using System;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using System.Web;
using NGeoIP;
using NGeoIP.Client;
using System.Data.Entity;
using RedirectApplication.RedirectDB;
using RedirectApplication.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace RedirectApplication.Controllers
{
    public class ValuesController : ApiController
    {
        public static string RandomString(int size) //чисто ради того, чтобы заполнить таблицу хоть чем-то
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }


        // GET api/values
        public HttpResponseMessage Get()
        {
            var userAgent = Request.Headers.UserAgent.ToString();
            var browser = HttpContext.Current.Request.Browser.Browser.ToString();
            var browser1 = HttpContext.Current.Request.Browser.Type.ToString();
            var url = HttpContext.Current.Request.RawUrl.ToString();
            var userIP = HttpContext.Current.Request.UserHostAddress.ToString();

            var nGeoRequest = new Request()
            {
                Format = Format.Json,
                IP = "91.144.189.179"
            };

            var nGeoClient = new NGeoClient(nGeoRequest);
            StringBuilder result = new StringBuilder("");
            var rawData = nGeoClient.Execute();
            using (var db = new RedirectContext())
            {
                var data = DateTime.Now.ToString();
                String ID_URL = RandomString(15);
                var redir = new RedirectRule { Id = ID_URL, Data = data };
                db.RedirectRules.Add(redir);
                db.SaveChanges();

                var query = from b in db.RedirectRules
                            orderby b.Data
                            select b;

                foreach (var item in query)
                {
                    result.Append("\n" + item.Id + "\t" + item.Data);
                }
            }

            //string result = "Hello! The Result is:\nUserAgent: " + userAgent + "\nBrowser: " + browser1 + "\nURL: " + url + "\nIP: " + userIP + "\nCity: " + rawData.City + "\nTime: " + DateTime.Now;
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent(result.ToString(), System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
