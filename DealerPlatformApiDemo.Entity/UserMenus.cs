using System;
using DealerPlatformApiDemo.Core;

namespace DealerPlatformApiDemo.Entity
{
    [Serializable]
    public class UserMenus : BaseEntity
    {
        public System.Int32 UserId { get; set; }
        public System.Int32 MenuId { get; set; }
    }
}