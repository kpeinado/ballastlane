using BallastlaneBLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BallastlaneAPI.Security
{
    public class BallastlaneAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var header = context.HttpContext.Request.Query["Authorization"].ToString();
            if (string.IsNullOrEmpty(header))
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                try
                {
                    var time = DateTime.Parse(Crypto.Decrypt(header));
                    if (time < DateTime.Now)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
                catch (Exception ex)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
