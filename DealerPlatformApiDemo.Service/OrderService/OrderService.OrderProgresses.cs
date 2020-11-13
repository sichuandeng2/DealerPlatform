using System;
using System.Collections.Generic;
using System.Linq;
using DealerPlatformApiDemo.Entity;
using static DealerPlatformApiDemo.Core.Data.RepositoryCompare;

namespace DealerPlatformApiDemo.Service.OrderService
{
    public partial class OrderService
    {
        /// <summary>
        /// 添加工作流
        /// </summary>
        /// <param name="orderNo"></param>
        private void AddProgress(string orderNo)
        {
            SaleOrderProgresses progress = new SaleOrderProgresses
            {
                ProgressGuid = Guid.NewGuid().ToString(),
                SaleOrderNo = orderNo,
                StepName = "下单",
                StepSn = 1,
                StepTime = DateTime.Now
            };
            _orderProgressRepository.Insert(progress);
        }
        /// <summary>
        /// 根据订单号获取订单进度
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public IEnumerable<SaleOrderProgresses> GetProgressByOrderNos(params string[] orderNo)
        {
            var progress = _orderProgressRepository.ListByCustomWhereIn("SaleOrderNo", orderNo);
            return progress;
        }

        public int SetOrderProgress(SaleOrderProgresses progress)
        {
            return _orderProgressRepository.Update(progress);
        }
        /// <summary>
        /// 根据订单进度获取进度列表
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public IEnumerable<SaleOrderProgresses> GetProgressByStepSn(string StepSn)
        {
            var progress = _orderProgressRepository.ListByCustom("StepSn", StepSn, CompareType.lessThan);
            return progress;
        }
        public bool NexTStep(string orderNo)
        {
            var progress = GetProgressByOrderNos(orderNo).FirstOrDefault();
            progress.StepSn += progress.StepSn < 5 ? 1 : 0;
            switch (progress.StepSn)
            {
                case 1:
                    {
                        progress.StepName = "下单";
                        break;
                    }
                case 2:
                    {
                        progress.StepName = "审核";
                        break;
                    }
                case 3:
                    {
                        progress.StepName = "发货";
                        break;
                    }
                case 4:
                    {
                        progress.StepName = "出库";
                        break;
                    }
                case 5:
                    {
                        progress.StepName = "收货";
                        break;
                    }
                default:
                    break;
            }
            progress.StepTime = DateTime.Now;
            int rowCount = SetOrderProgress(progress);
            return rowCount > 0 ? true : false;
        }
    }
}