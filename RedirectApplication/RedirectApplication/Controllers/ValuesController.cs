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
using Newtonsoft.Json.Linq;

namespace RedirectApplication.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            var user = new UsersAttributes();

            user.Url = HttpContext.Current.Request.RawUrl.ToString();
            user.Browser = HttpContext.Current.Request.Browser.Browser.ToString(); //Which browser is using //http://www.codeproject.com/Articles/1088703/How-to-detect-browsers-in-ASP-NET-with-browser-fil#_comments
            user.OS = HttpContext.Current.Request.Browser.Platform.ToString(); ///Which OS is using
            user.MobileOrNot = HttpContext.Current.Request.Browser.IsMobileDevice.ToString(); //true - request was made by Mobile device
            user.UserIP = HttpContext.Current.Request.UserHostAddress.ToString(); //Now it`s ::1 because it's running locally
            user.Language = Request.Headers.AcceptLanguage.ToString().Substring(0, 2); //The most used language
            var nGeoRequest = new Request()
            {
                Format = Format.Json,
                IP = "91.144.189.179" //here is specified ip address because it's running locally
            };
            var nGeoClient = new NGeoClient(nGeoRequest);
            var rawData = nGeoClient.Execute();
            user.Country = rawData.CountryName.ToString(); //The country where the request was made
            user.Time = DateTime.Now.ToString(); //The time when the request was made

            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            return resp;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            HttpResponseMessage resp;
            try
            {
                var content = Deserialization(request);
                if (content == null)
                {
                    throw new Exception("Something wrong with structure");
                }
                if (content.TargetUrl == null)
                {
                    throw new Exception("No TargetUrl");
                }
                if (content.Conditions == null)
                {
                    throw new Exception("Something wrong with Conditions");
                }
                foreach (var field in content.Conditions)
                {
                    if (field is Composite)
                    {
                        var composite = field as Composite;
                        if (composite.Rules == null)
                            throw new Exception("Rules in Composite are incorrect");
                        if (composite.Url == null)
                            throw new Exception("Url in Composite is incorrect");
                        foreach (var ffield in composite.Rules)
                        {
                            if (ffield is Composite)
                                throw new Exception("Composite in Composite");
                            if (ffield is ByBrowser)
                            {
                                var Cbrowser = ffield as ByBrowser;
                                if (Cbrowser.Url != null)
                                    throw new Exception("There is an Url in Composite`s Browser");
                                if (Cbrowser.Browser == null)
                                    throw new Exception("Browser name is incorrect");
                            }
                            if (ffield is ByCountry)
                            {
                                var Ccountry = ffield as ByCountry;
                                if (Ccountry.Url != null)
                                    throw new Exception("There is an Url in Composite`s Country");
                                if (Ccountry.Country == null)
                                    throw new Exception("Country name is incorrect");
                            }
                        }
                    }
                    if (field is ByBrowser)
                    {
                        var bybrowser = field as ByBrowser;
                        if (bybrowser.Url == null)
                            throw new Exception("Url in Browser is incorrect");
                        if (bybrowser.Browser == null)
                            throw new Exception("Browser is incorrect");
                    }
                }
                resp = new HttpResponseMessage(HttpStatusCode.OK);
                return resp;
            }
            catch (FormatException fe)
            {
                resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                resp.Content = new StringContent(fe.Message.ToString(), System.Text.Encoding.UTF8, "text/plain");
                return resp;
                //Error print etc..
            }
            catch (Exception e)
            {
                resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                resp.Content = new StringContent(e.Message.ToString(), System.Text.Encoding.UTF8, "text/plain");
                return resp;
                //Error print etc..
            }
        }

        private PostJson Deserialization(HttpRequestMessage request)
        {
                var someText = request.Content.ReadAsStringAsync().Result;
                var reader = new JsonTextReader(new StringReader(someText));
                PostJson content = JsonSerializer.CreateDefault().Deserialize<PostJson>(reader);
                return content;
        }
        private bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
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
