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

            user.Url = HttpContext.Current.Request.Url.ToString();
            if (!(new RedirectDB.RedirectRepository()).IsRuleExist(user.Url))
                return new HttpResponseMessage(HttpStatusCode.NotFound);
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
            user.Time = DateTime.Now.ToString(); //The time when the request was made
            var redirectUrl = redirect.VerificationByRules(user);

            if (redirectUrl == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
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
                        if ((composite.Url == null) || (composite.Url == ""))
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
                                if ((Cbrowser.Browser == null) || (Cbrowser.Browser == ""))
                                    throw new Exception("Browser name is incorrect");
                                continue;
                            }
                            if (ffield is ByCountry)
                            {
                                var Ccountry = ffield as ByCountry;
                                if (Ccountry.Url != null)
                                    throw new Exception("There is an Url in Composite`s Country");
                                if ((Ccountry.Country == null) || (Ccountry.Country == ""))
                                    throw new Exception("Country name is incorrect");
                                continue;
                            }
                            if (ffield is ByLanguage)
                            {
                                var Clanguage = ffield as ByLanguage;
                                if (Clanguage.Url != null)
                                    throw new Exception("There is an Url in Composite`s Language");
                                if ((Clanguage.Language == null) || (Clanguage.Language == ""))
                                    throw new Exception("Language name is incorrect");
                                continue;
                            }
                            if (ffield is ByIp)
                            {
                                var Cip = ffield as ByIp;
                                if (Cip.Url != null)
                                    throw new Exception("There is an Url in Composite`s IP");
                                if ((Cip.Ip == null) || (Cip.Ip[0] >= Cip.Ip[1]) || (Cip.Ip.Length != 2))
                                    throw new Exception("IP interval is incorrect");
                                continue;
                            }
                            if (ffield is ByOS)
                            {
                                var Cos = ffield as ByOS;
                                if (Cos.Url != null)
                                    throw new Exception("There is an Url in Composite`s OS");
                                if ((Cos.OS == null) || (Cos.OS == ""))
                                    throw new Exception("OS name is incorrect");
                                continue;
                            }
                            if (ffield is ByDevice)
                            {
                                var Cdevice = ffield as ByDevice;
                                if (Cdevice.Url != null)
                                    throw new Exception("There is an Url in Composite`s Device");
                                continue;
                            }
                            if (ffield is ByDate)
                            {
                                var Cdate = ffield as ByDate;
                                if (Cdate.Url != null)
                                    throw new Exception("There is an Url in Composite`s Date");
                                if ((Cdate.Date == null) || (Cdate.Date == ""))
                                    throw new Exception("Date is incorrect");
                                continue;
                            }
                            throw new Exception("Unknown element in Composite");
                        }
                        continue;
                    }
                    if (field is ByBrowser)
                    {
                        var bybrowser = field as ByBrowser;
                        if ((bybrowser.Url == null) || (bybrowser.Url == ""))
                            throw new Exception("Url in Browser is incorrect");
                        if ((bybrowser.Browser == null) || (bybrowser.Browser == ""))
                            throw new Exception("Browser is incorrect");
                        continue;
                    }
                    if (field is ByCountry)
                    {
                        var bycountry = field as ByCountry;
                        if ((bycountry.Url == null) || (bycountry.Url == ""))
                            throw new Exception("Url in Country is incorrect");
                        if ((bycountry.Country == null) || (bycountry.Country == ""))
                            throw new Exception("Country name is incorrect");
                        continue;
                    }
                    if (field is ByLanguage)
                    {
                        var bylanguage = field as ByLanguage;
                        if ((bylanguage.Url == null) || (bylanguage.Url == ""))
                            throw new Exception("Url in Language is incorrect");
                        if ((bylanguage.Language == null) || (bylanguage.Language == ""))
                            throw new Exception("Language name is incorrect");
                        continue;
                    }
                    if (field is ByIp)
                    {
                        var byip = field as ByIp;
                        if ((byip.Url == null) || (byip.Url == ""))
                            throw new Exception("Url in IP is incorrect");
                        if ((byip.Ip == null) || (byip.Ip[0] >= byip.Ip[1]) || (byip.Ip.Length != 2))
                            throw new Exception("IP interval is incorrect");
                        continue;
                    }
                    if (field is ByOS)
                    {
                        var byos = field as ByOS;
                        if ((byos.Url == null) || (byos.Url == ""))
                            throw new Exception("Url in OS is incorrect");
                        if ((byos.OS == null) || (byos.OS == ""))
                            throw new Exception("OS name is incorrect");
                        continue;
                    }
                    if (field is ByDevice)
                    {
                        var bydevice = field as ByDevice;
                        if ((bydevice.Url == null) || (bydevice.Url == ""))
                            throw new Exception("Url in Device is incorrect");
                        continue;
                    }
                    if (field is ByDate)
                    {
                        var bydate = field as ByDate;
                        if ((bydate.Url == null) || (bydate.Url == ""))
                            throw new Exception("Url in Date is incorrect");
                        if ((bydate.Date == null) || (bydate.Date == ""))
                            throw new Exception("Date is incorrect");
                        continue;
                    }
                    throw new Exception("Unknown element in Conditions");
                }
                resp = new HttpResponseMessage(HttpStatusCode.OK);
                return resp;
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
