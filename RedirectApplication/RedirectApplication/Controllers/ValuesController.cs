using System;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using System.Web;
using NGeoIP;
using NGeoIP.Client;
using RedirectApplication.Models;
using Newtonsoft.Json;
using RedirectApplication.RedirectMaker;

namespace RedirectApplication.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            var user = new UsersAttributes();
            var redirect = new Redirect();

            user.Url = HttpContext.Current.Request.RawUrl.ToString();
            user.Browser = HttpContext.Current.Request.Browser.Browser.ToString(); //Which browser is using //http://www.codeproject.com/Articles/1088703/How-to-detect-browsers-in-ASP-NET-with-browser-fil#_comments
            user.OS = HttpContext.Current.Request.Browser.Platform.ToString(); ///Which OS is using
            user.MobileOrNot = HttpContext.Current.Request.Browser.IsMobileDevice; //true - request was made by Mobile device
            user.UserIP = BitConverter.ToUInt32(IPAddress.Parse(HttpContext.Current.Request.UserHostAddress).GetAddressBytes(), 0); //Now it`s ::1 because it's running locally
            user.Language = Request.Headers.AcceptLanguage.ToString().Substring(0, 2); //The most used language
            var nGeoRequest = new Request()
            {
                Format = Format.Json,
                IP = "91.144.189.179" //here is specified ip address because it's running locally
            };
            var nGeoClient = new NGeoClient(nGeoRequest);
            var rawData = nGeoClient.Execute();
            user.Country = rawData.CountryName.ToString(); //The country where the request was made
            user.Time = DateTime.Now.ToString(); //The time when the request was made+

            var redirectUrl = redirect.VerificationByRules(user);

            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            return resp;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(HttpRequestMessage request)
        {
            var content = Deserialization(request);
        }

        private PostJson Deserialization(HttpRequestMessage request)
        {
            var someText = request.Content.ReadAsStringAsync().Result;
            var reader = new JsonTextReader(new StringReader(someText));
            PostJson content = JsonSerializer.CreateDefault().Deserialize<PostJson>(reader);
            return content;
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
