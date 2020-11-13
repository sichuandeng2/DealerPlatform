using System.Net;
using DealerPlatformApiDemo.Common.JwtHelper;
using DealerPlatformApiDemo.Service.CustomerService;
using DealerPlatformApiDemo.Service.CustomerService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerPlatformApiDemo.Api.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ICustomer _customerService;

        public LoginController(ICustomer customerService) {
            this._customerService = customerService;
        }
        [HttpPost]
        public string CheckLogin(UserCheckInput userCheckInput) {
            if (string.IsNullOrEmpty(userCheckInput.CustomerName) || string.IsNullOrEmpty(userCheckInput.Pwd)) {
                this.HttpContext.Response.StatusCode = 204;
                return null;
            }
            var customerPwd = _customerService.CheckLogin(userCheckInput);
            if (customerPwd != null)
            {
                return GetToken(customerPwd.CustomerNo) ;
            }
            else
            {
                this.HttpContext.Response.StatusCode = 204;
                return null;
            }
        }
        [HttpGet]
        public string GetToken(string cid){
            TokenCreateModel token = new TokenCreateModel(){
                Audience = "WebApi",
                Issuer = "webapi.cn",
                SecurityKey="1234567890987654321",
                UserId = cid,
                ts = TimeSpan.FromHours(2)
            };
            return TokenHelper.CreateToken(token);
        }
        // private string GetToken(string cid){
        //  using(WebClient wc = new WebClient()){
        //    return  wc.DownloadString("http://localhost:5001/token/"+cid);
        //  }
        // }
    }
}
