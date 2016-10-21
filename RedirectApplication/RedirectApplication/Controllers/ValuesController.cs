using System;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web;
using NGeoIP;
using NGeoIP.Client;
using RedirectApplication.Json;
using RedirectApplication.Models;

namespace RedirectApplication.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            var browser = HttpContext.Current.Request.Browser.Browser.ToString(); //Which browser is using //http://www.codeproject.com/Articles/1088703/How-to-detect-browsers-in-ASP-NET-with-browser-fil#_comments
            var OS = HttpContext.Current.Request.Browser.Platform.ToString(); ///Which OS is using
            var MobileOrNot = HttpContext.Current.Request.Browser.IsMobileDevice.ToString(); //true - request was made by Mobile device
            var userIP = HttpContext.Current.Request.UserHostAddress.ToString(); //Now it`s ::1 because it's running locally
            var language = Request.Headers.AcceptLanguage.ToString().Substring(0, 2); //The most used language
            var nGeoRequest = new Request()
            {
                Format = Format.Json,
                IP = "91.144.189.179" //here is specified ip address because it's running locally
            };
            var nGeoClient = new NGeoClient(nGeoRequest);
            var rawData = nGeoClient.Execute();
            var Country = rawData.CountryName.ToString(); //The country where the request was made
            var Time = DateTime.Now.ToString(); //The time when the request was made
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
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
            DbJson test = new DbJson();
            
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
