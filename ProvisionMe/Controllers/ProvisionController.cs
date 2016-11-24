using ProvisionMe.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Web.Http;
using System.Web.Http.Results;

namespace ProvisionMe.Controllers
{
    public class ProvisionController : ApiController
    {
        // POST api/<controller>            
        public JsonResult<ProvisionResponse> Post(PermissionRequest data)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("serviceUrl"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP JSON POST
                var response = client.PostAsJsonAsync<PermissionRequest>("api/provision", data);

                response.Wait();

                var contentTask = response.Result.Content.ReadAsAsync<ProvisionResponse>();

                contentTask.Wait();

                return Json<ProvisionResponse>(contentTask.Result);
            }
        }
    }
}