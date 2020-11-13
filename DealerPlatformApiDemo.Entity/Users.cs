using System;
using DealerPlatformApiDemo.Core;

namespace DealerPlatformApiDemo.Entity
{
    [Serializable]
    public class Users : BaseEntity
    {
        public System.Guid UserGuid { get; set; }
        public System.String UserName { get; set; }
        public System.String Password { get; set; }
        public System.String HeadUrl { get; set; }
        public System.Boolean IsDel { get; set; }
    }
}