using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Nicacio.MinhaApi.Api.Provider
{
    public class SimpleAuthServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Invocado quando a api precisa validar se o cliente tem permissão para acessar
        /// o owin trabalha de forma async
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            //return base.ValidateClientAuthentication(context);
        }

        /// <summary>
        /// verifica se o usuario e a senha enviados para a geração do token são validos
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new string[] { "*" });
            var usuario = context.UserName;
            var senha = context.Password;
            if (usuario != "1234" || senha != "1234")
            {
                context.SetError("invalid_user_or_password", "Usuário e/ou senha incorretos");
                return;
            }
            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identity);
            //return base.GrantResourceOwnerCredentials(context);
        }
    }
}