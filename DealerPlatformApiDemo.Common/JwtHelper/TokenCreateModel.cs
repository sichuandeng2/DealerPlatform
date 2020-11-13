using System;

namespace DealerPlatformApiDemo.Common.JwtHelper
{
    public class TokenCreateModel
    {
        public string UserId { get; set; }
        //密钥
        public string SecurityKey { get; set; }
        //签发人的名字
        public string Issuer { get; set; }
        //观众
        public string Audience { get; set; }
        //过期时间
        public TimeSpan ts { get; set; }
    }
}