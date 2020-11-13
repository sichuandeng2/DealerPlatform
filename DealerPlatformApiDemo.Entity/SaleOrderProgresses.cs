using System;
using DealerPlatformApiDemo.Core;

namespace DealerPlatformApiDemo.Entity
{
  [Serializable]
    public class SaleOrderProgresses:BaseEntity
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public SaleOrderProgresses()
        { }

        private System.Int32 _Id;
        private System.String _SaleOrderNo;
        private System.String _ProgressGuid;
        private System.Int32 _StepSn;
        private System.String _StepName;
        private System.DateTime _StepTime;

        /// <summary>
        /// Id属性封装
        /// </summary>
        public System.Int32 Id
        {
            set { _Id = value; }
            get { return _Id; }
        }
        /// <summary>
        /// SaleOrderNo属性封装
        /// </summary>
        public System.String SaleOrderNo
        {
            set { _SaleOrderNo = value; }
            get { return _SaleOrderNo; }
        }
        /// <summary>
        /// ProgressGuid属性封装
        /// </summary>
        public System.String ProgressGuid
        {
            set { _ProgressGuid = value; }
            get { return _ProgressGuid; }
        }
        /// <summary>
        /// StepSn属性封装
        /// </summary>
        public System.Int32 StepSn
        {
            set { _StepSn = value; }
            get { return _StepSn; }
        }
        /// <summary>
        /// StepName属性封装
        /// </summary>
        public System.String StepName
        {
            set { _StepName = value; }
            get { return _StepName; }
        }
        /// <summary>
        /// StepTime属性封装
        /// </summary>
        public System.DateTime StepTime
        {
            set { _StepTime = value; }
            get { return _StepTime; }
        }
    }
}