using Microsoft.IdentityModel.Clients.ActiveDirectory;
using ProvisionMe.Controllers;
using ProvisionMe.Models;
using System;
using System.Configuration;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ProvisionMe
{
    public partial class Provision : System.Web.UI.Page
    {
        private void GetClaims()
        {
            ////get the tenantName
            //string tenantName = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;

            //string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            //AuthenticationContext authContext = new AuthenticationContext(Startup.Authority,                new NaiveSessionCache(userObjectID));
            //ClientCredential credential = new ClientCredential(clientId, appKey);
            //result = authContext.AcquireTokenSilent(graphResourceId, credential,
            //    new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));



            //// retrieve the clientId and password values from the Web.config file
            //string clientId = ConfigurationManager.AppSettings["ClientId"];
            //string password = ConfigurationManager.AppSettings["Password"];



            //// get a token using the helper
            //AADJWTToken token = DirectoryDataServiceAuthorizationHelper.GetAuthorizationToken(tenantName, clientId, password);

            //// initialize a graphService instance using the token acquired from previous step
            //DirectoryDataService graphService = new DirectoryDataService(tenantName, token);

            ////  get Users
            ////
            //var users = graphService.users;
            //QueryOperationResponse<User> response;
            //response = users.Execute() as QueryOperationResponse<User>;
            //List<User> userList = response.ToList();
            //ViewBag.userList = userList;


            ////  For subsequent Graph Calls, the existing token should be used.
            ////  The following checks to see if the existing token is expired or about to expire in 2 mins
            ////  if true, then get a new token and refresh the graphService
            ////
            //int tokenMins = 2;
            //if (token.IsExpired || token.WillExpireIn(tokenMins))
            //{
            //    AADJWTToken newToken = DirectoryDataServiceAuthorizationHelper.GetAuthorizationToken(tenantName, clientId, password);
            //    token = newToken;
            //    graphService = new DirectoryDataService(tenantName, token);
            //}

            ////  get tenant information
            ////
            //var tenant = graphService.tenantDetails;
            //QueryOperationResponse<TenantDetail> responseTenantQuery;
            //responseTenantQuery = tenant.Execute() as QueryOperationResponse<TenantDetail>;
            //List<TenantDetail> tenantInfo = responseTenantQuery.ToList();
            //ViewBag.OtherMessage = "User List from tenant: " + tenantInfo[0].displayName;


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                //var token = FederatedAuthentication.SessionAuthenticationModule.CreateSessionSecurityToken( Thread.CurrentPrincipal, "test", from, to, true);

                //FederatedAuthentication.SessionAuthenticationModule.AuthenticateSessionSecurityToken(token, true);

                //GetClaims();

                //var appPoolIdentity = WindowsIdentity.GetCurrent().Name;

                var userIdentity = Thread.CurrentPrincipal.Identity.Name;

                txtUserName.Text = userIdentity;
            }

            if (!IsPostBack)
            {
                //lstMSQRoles.Items.Add(new ListItem { Text = "MQ1", Value = "MQ1" });
                //lstMSQRoles.Items.Add(new ListItem { Text = "MQ2", Value = "MQ2" });
                //lstMSQRoles.Items.Add(new ListItem { Text = "MQ3", Value = "MQ3" });
                //lstMSQRoles.Items.Add(new ListItem { Text = "MQ4", Value = "MQ4", Selected = true });
                //lstMSQRoles.Items.Add(new ListItem { Text = "QSU", Value = "QSU" });
                //lstMSQRoles.Items.Add(new ListItem { Text = "MQF", Value = "MQF" });
                //lstMSQRoles.Items.Add(new ListItem { Text = "QV", Value = "QV" });
            }
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("serviceUrl"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                var response = client.GetAsync("api/environment");

                response.Wait();

                var contentTask = response.Result.Content.ReadAsAsync<EnvironmentDataModel[]>();

                contentTask.Wait();

                //var x = ActiveDirectoryHelper.GetValue("redmond\\alberbel", "mail");

                var index = lstEnvironment.SelectedIndex;

                lstEnvironment.Items.Clear();

                foreach (var item in contentTask.Result)
                {
                    var listItem = new ListItem { Text = item.Name, Value = item.Name };

                    listItem.Attributes.Add("hasMOET", item.hasMoet.ToString().ToLower());
                    listItem.Attributes.Add("hasMOPET", item.hasMopet.ToString().ToLower());

                    lstEnvironment.Items.Add(listItem);
                }

                lstEnvironment.SelectedIndex = index;
            }
        }

        //protected void lstApplication_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Label1.Text = "";
        //    Label2.Text = "";
        //}
    }

    public static class Extensions
    {
        public static string ToRole(this HtmlInputCheckBox source)
        {
            return (source.Checked ? "I" : "D");
        }
    }
}