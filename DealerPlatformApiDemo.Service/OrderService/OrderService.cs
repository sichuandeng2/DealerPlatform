using AutoMapper;
using DealerPlatformApiDemo.Core.Data;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.ShoppingCartService;

namespace DealerPlatformApiDemo.Service.OrderService
{
    public partial class OrderService:IOrderService
    {
        private readonly IRepository<SaleOrderMasters> _orderMasterRepository;
        private readonly IRepository<SaleOrderDetails> _orderDetailRepository;
        private readonly IRepository<SaleOrderProgresses> _orderProgressRepository;
        private readonly IRepository<CustomerInvoices> _customerInvoices;
        private readonly IShoppingCart _cartService;
        private readonly IMapper _mapper;

        public OrderService(
            IRepository<SaleOrderMasters> orderMasterRepository,
            IRepository<SaleOrderDetails> orderDetailRepository,
            IRepository<SaleOrderProgresses> orderProgressRepository,
            IRepository<CustomerInvoices> customerInvoices,
            IShoppingCart cartService,
            IMapper mapper
            )
        {
            this._orderMasterRepository = orderMasterRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._orderProgressRepository = orderProgressRepository;
            this._customerInvoices = customerInvoices;
            this._cartService = cartService;
            this._mapper = mapper;
        }
    }
}