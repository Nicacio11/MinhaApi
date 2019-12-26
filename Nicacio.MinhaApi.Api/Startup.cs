using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Nicacio.MinhaApi.Api.Provider;
using Owin;

[assembly: OwinStartup(typeof(Nicacio.MinhaApi.Api.Startup))]

namespace Nicacio.MinhaApi.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ///configurando http
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthOptions = new OAuthAuthorizationServerOptions
            {
                //nunca utilizar true em produção, pois estará permitindo o processo de autenticação utilize o htttp
                //mas é preciso de um certificado para que seja utilizado a criptografia das informações
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(50),
                Provider = new SimpleAuthServerProvider()
            };

            app.UseOAuthAuthorizationServer(oAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
