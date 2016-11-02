using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin;
using Microsoft.Owin.Testing;
using RedirectApplication;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Owin.Host.HttpListener;

namespace Tests
{
    [TestClass]
    public class RedirectTests
    {
        public static string baseAddress = "http://*:55129/";
        private static IDisposable _webApp;
        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            if (typeof(OwinHttpListener) != null)
            {
                _webApp = WebApp.Start<Startup>(url: baseAddress);
            }
        }
        [AssemblyCleanup]
        public static void TearDown()
        {
            _webApp.Dispose();
        }
        //[TestMethod()]
        //public void GetMethod()
        //{
        //    using (var webApp = WebApp.Start<Startup>("http://localhost:55129/"))
        //    {
        //        // Execute test against the web API.
        //        webApp.Dispose();
        //    }
        //}
        //[TestMethod]
        //public async Task TestMethod()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var requestUri = new Uri("http://localhost:55129/");
        //        await httpClient.GetStringAsync(requestUri);
        //    }
        //}
        [TestMethod]
        public void GetMethod()
        {

        }

    }
}
