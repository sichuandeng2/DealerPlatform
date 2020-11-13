using System;
using DealerPlatformApiDemo.Core;

namespace DealerPlatformApiDemo.Entity
{
    [Serializable]
    public class Menus : BaseEntity
    {
        public System.String MenuName { get; set; }
        public System.String MenuUrl { get; set; }
        public System.String ActionName { get; set; }
        public System.Int32? ParentMenuId { get; set; }
    }
}