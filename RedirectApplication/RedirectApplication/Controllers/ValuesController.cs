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
namespace RedirectApplication.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            var browser = HttpContext.Current.Request.Browser.Browser.ToString(); //Какой браузер использует пользователь
            var OS = HttpContext.Current.Request.Browser.Platform.ToString(); ///Какая ОС используется пользователем
            var MobileOrNot = HttpContext.Current.Request.Browser.IsMobileDevice.ToString(); //true - пользователь сидить с мобильного устройства. иначе - false
            var userIP = HttpContext.Current.Request.UserHostAddress.ToString(); //IP пользователя. Пока что определяется как ::1 т.к. локалка
            var language = Request.Headers.AcceptLanguage.ToString().Substring(0, 2); //Наиболее используемый пользователем язык
            var nGeoRequest = new Request()
            {
                Format = Format.Json,
                IP = "91.144.189.179" //Из-за того, что локалка - придется пока что использовать заранее указанный IP
            };
            var nGeoClient = new NGeoClient(nGeoRequest);
            var rawData = nGeoClient.Execute();
            var Country = rawData.CountryName.ToString(); //Страна пользователя
            var Time = DateTime.Now.ToString(); //Время запроса пользователя
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
