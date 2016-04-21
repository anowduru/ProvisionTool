using ProvisionMe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ProvisionMe.Controllers
{
    public class EnvironmentController : ApiController
    {
        public EnvironmentDataModel[] Get()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("serviceUrl"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP JSON POST
                var response = client.GetAsync("api/environment");

                response.Wait();

                var contentTask = response.Result.Content.ReadAsAsync<EnvironmentDataModel[]>();

                contentTask.Wait();

                return contentTask.Result;
            }
        }
    }
}
