using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.OrderService.Dto;
using DealerPlatformApiDemo.Service.ShoppingCartService.Dto;

namespace DealerPlatformApiDemo.Service.OrderService
{
    public partial class OrderService
    {
        public string AddOrder(string custormerNo,
            OrderMasterInputDto inputDto,
            List<ShoppingCartDto> cartsDto,
            List<ProductSales> productSales,
            List<ProductPhotos> productPhotos,
            List<Products> products)
        {
            //将这三部包含在事务中
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    //添加主订单
                    DateTime inputDate = DateTime.Now;
                    string orderNo = Guid.NewGuid().ToString();
                    SaleOrderMasters master = new SaleOrderMasters
                    {
                        CustomerNo = custormerNo,
                        DeliveryDate = inputDto.DeliveryDate,
                        EditUserNo = custormerNo,
                        InputDate = inputDate,
                        InvoiceNo = inputDto.InvoiceNo,
                        Remark = inputDto.Remark,
                        SaleOrderNo = orderNo,
                        StockNo = "",
                    };
                    _orderMasterRepository.Insert(master);
                    //添加订单流程
                    AddProgress(orderNo);
                    //添加订单详情（再订单详情中 删除相应物品的购物车信息）
                    var carts = _mapper.Map<List<ShoppingCarts>>(cartsDto);
                    AddOrderDetail(custormerNo, carts, productSales, productPhotos, products, inputDate, orderNo);
                    ts.Complete();
                    return orderNo;
                }
                catch (System.Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
            }
        }
        public SaleOrderDto GetOrderInfoByNo(string saleOrderNo)
        {
            var orderMaster = _orderMasterRepository.ListByCustom("SaleOrderNo", saleOrderNo).FirstOrDefault();
            var orderDto = _mapper.Map<SaleOrderDto>(orderMaster);
            orderDto.OrderProgresses = GetProgressByOrderNos(saleOrderNo).FirstOrDefault();
            orderDto.OrderDetails = GetOrderDetailByOrderNo(saleOrderNo);
            orderDto.CustomerInvoices = _customerInvoices.ListById(Convert.ToInt32(orderDto.InvoiceNo)).FirstOrDefault();
            return orderDto;
        }
        public IEnumerable<SaleOrderDto> GetOrderInfoByCustomerNo(string customerNo)
        {
            var masters = _orderMasterRepository.ListByCustom("CustomerNo", customerNo);
            var orderDtos = _mapper.Map<List<SaleOrderDto>>(masters);
            foreach (var orderDto in orderDtos)
            {
                orderDto.OrderDetails = GetOrderDetailByOrderNo(orderDto.SaleOrderNo);
            }
            return orderDtos;
        }
        public int GetUnFinishedOrdersNum(string custormerNo)
        {
            var orderMaster = _orderMasterRepository.ListByCustom("CustomerNo", custormerNo);
            var orderNos = orderMaster.Select(m => m.SaleOrderNo);
            return _orderProgressRepository.ListByCustomWhereIn("SaleOrderNo", orderNos.ToArray())
            .Count(m => m.StepSn < 4);
        }
        public List<SaleOrderDto> ListOrderInfoByStepSn(string StepSn)
        {
            var progress = GetProgressByStepSn("5");
            var orderMasters = _orderMasterRepository.ListByCustomWhereIn("SaleOrderNo", progress.Select(m => m.SaleOrderNo).ToArray());
            var orderDtos = _mapper.Map<List<SaleOrderDto>>(orderMasters);
            foreach (var orderDto in orderDtos)
            {
                orderDto.OrderProgresses = progress.FirstOrDefault(m => m.SaleOrderNo == orderDto.SaleOrderNo);
            }
            return orderDtos;
        }
    }
}