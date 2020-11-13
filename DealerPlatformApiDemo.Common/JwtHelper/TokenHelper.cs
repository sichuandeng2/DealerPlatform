using System.Text;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DealerPlatformApiDemo.Common.JwtHelper
{
    public class TokenHelper
    {
        public static string CreateToken(TokenCreateModel model){
            //1.放置需要给前端携带的信息
            //把用户ID加入一个Claims中，这样可以识别用户且携带用户Id信息
            var claims = new[]{
               new Claim("UserId",model.UserId)
            };
            //2.生成密钥
            //使用密钥对令牌进行签名，这个令牌会在所有我需要对该令牌检验处共享
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(model.SecurityKey));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            //3.组合token所需要的信息
            //组成token需要的所有信息
            var token = new JwtSecurityToken(
                issuer : model.Issuer,
                audience : model.Audience,
                claims : claims,
                expires: DateTime.Now.Add(model.ts),
                signingCredentials:creds
            );
            //4.生产token
            var accesToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accesToken;
        }
    }
}