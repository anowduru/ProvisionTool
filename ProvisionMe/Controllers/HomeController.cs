using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProvisionMe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Provision()
        {
            return Redirect("~/Provision.aspx");
        }

        public void Signout()
        {
            //WsFederationConfiguration fc = FederatedAuthentication.FederationConfiguration.WsFederationConfiguration;

            //string request = System.Web.HttpContext.Current.Request.Url.ToString();
            //string wreply = request.Substring(0, request.Length - 7);

            //SignOutRequestMessage soMessage = new SignOutRequestMessage(new Uri(fc.Issuer), wreply);
            //soMessage.SetParameter("wtrealm", fc.Realm);

            //FederatedAuthentication.SessionAuthenticationModule.SignOut();

            //var data = soMessage.WriteQueryString();

            //Response.Redirect(data);           
        }
    }
}
