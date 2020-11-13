using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DealerPlatformApiDemo.Api.Filters
{
    public class CtmAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var claimsIdentity = (ClaimsIdentity)user.Identity;
            foreach (var claim in claimsIdentity.Claims)
            {
                if(claim.Type =="UserId"){
                    context.HttpContext.Items.Add("userNo",claim.Value);
                }
            }
        }
    }
}