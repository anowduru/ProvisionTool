using System;
using System.IdentityModel.Services;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProvisionMe
{
    public class WebApiApplication : System.Web.HttpApplication
    {        
        void Application_Start(object sender, EventArgs e)
        {           
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //FederatedAuthentication.SessionAuthenticationModule.SessionSecurityTokenCreated += new EventHandler<SessionSecurityTokenCreatedEventArgs>(SessionAuthenticationModule_SessionSecurityTokenCreated);
            //FederatedAuthentication.SessionAuthenticationModule.SessionSecurityTokenReceived += new EventHandler<SessionSecurityTokenReceivedEventArgs>(SessionAuthenticationModule_SessionSecurityTokenReceived);
            //FederatedAuthentication.SessionAuthenticationModule.SigningOut += new EventHandler<SigningOutEventArgs>(SessionAuthenticationModule_SigningOut);
            //FederatedAuthentication.SessionAuthenticationModule.SignedOut += new EventHandler(SessionAuthenticationModule_SignedOut);
            //FederatedAuthentication.SessionAuthenticationModule.SignOutError += new EventHandler<ErrorEventArgs>(SessionAuthenticationModule_SignOutError);
        }

        void SessionAuthenticationModule_SignOutError(object sender, ErrorEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Handling SignOutError event");
        }

        void SessionAuthenticationModule_SignedOut(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Handling SignedOut event");
        }

        void SessionAuthenticationModule_SigningOut(object sender, SigningOutEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Handling SigningOut event");
        }

        void SessionAuthenticationModule_SessionSecurityTokenReceived(object sender, SessionSecurityTokenReceivedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Handling SessionSecurityTokenReceived event");

            //var token = FederatedAuthentication.SessionAuthenticationModule.CreateSessionSecurityToken(e.SessionToken.ClaimsPrincipal, e.SessionToken.Context, e.SessionToken.ValidFrom, e.SessionToken.ValidTo, true);

            //FederatedAuthentication.SessionAuthenticationModule.AuthenticateSessionSecurityToken(token, true);
        }

        void SessionAuthenticationModule_SessionSecurityTokenCreated(object sender, SessionSecurityTokenCreatedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Handling SessionSecurityTokenCreated event");
            //Store session on the server-side token cache instead writing the whole token to the cookie. 
            //It may improve throughput but introduces server affinity that may affect scalability
            FederatedAuthentication.SessionAuthenticationModule.IsReferenceMode = true;
        }

        private void WSFederationAuthenticationModule_RedirectingToIdentityProvider(object sender, RedirectingToIdentityProviderEventArgs e)
        {
            if (!String.IsNullOrEmpty(IdentityConfig.Realm))
            {
                e.SignInRequestMessage.Realm = IdentityConfig.Realm;
            }
        }
    }
}
