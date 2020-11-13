USE [DealerPlatform]
GO
/****** Object:  Table [dbo].[CustomerInvoices]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerInvoices](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[CustomerNo] [varchar](40) NOT NULL,
	[InvoiceNo] [varchar](100) NOT NULL,
	[InvoiceEin] [varchar](40) NOT NULL,
	[InvoiceBank] [varchar](100) NOT NULL,
	[InvoiceAccount] [varchar](40) NOT NULL,
	[InvoiceAddress] [varchar](100) NOT NULL,
	[InvoicePhone] [varchar](40) NOT NULL,
 CONSTRAINT [PK_T200_customer_invoice] PRIMARY KEY CLUSTERED 
(
	[CustomerNo] ASC,
	[InvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerPwds]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPwds](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[CustomerNo] [varchar](40) NOT NULL,
	[CustomerPwd] [varchar](40) NOT NULL,
 CONSTRAINT [PK_T101_customer_psw] PRIMARY KEY CLUSTERED 
(
	[CustomerNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[CustomerNo] [varchar](40) NOT NULL,
	[CustomerName] [varchar](100) NOT NULL,
	[FirstAreaNo] [varchar](40) NOT NULL,
	[FirstAreaName] [varchar](100) NOT NULL,
	[AreaNo] [varchar](40) NOT NULL,
	[AreaName] [varchar](100) NOT NULL,
	[Province] [varchar](40) NOT NULL,
	[City] [varchar](40) NOT NULL,
	[Tel] [varchar](40) NOT NULL,
	[Phone] [varchar](40) NOT NULL,
	[Fax] [varchar](40) NOT NULL,
	[Address] [varchar](100) NOT NULL,
	[BankAccount] [varchar](40) NOT NULL,
	[BankNo] [varchar](40) NOT NULL,
	[BankName] [varchar](100) NOT NULL,
	[Ein] [varchar](40) NOT NULL,
	[CustomerNote] [varchar](500) NOT NULL,
	[OwnerWorkerNo] [varchar](40) NULL,
	[OwnerWorkerName] [varchar](100) NULL,
	[OwnerWorkerTel] [varchar](40) NULL,
	[OpenId] [varchar](40) NULL,
	[HeadImgUrl] [varchar](255) NULL,
 CONSTRAINT [PK_T200_customer_code] PRIMARY KEY CLUSTERED 
(
	[CustomerNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductPhotos]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductPhotos](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SysNo] [varchar](40) NULL,
	[ProductNo] [varchar](100) NOT NULL,
	[ProductPhotoUrl] [varchar](100) NOT NULL,
 CONSTRAINT [PK_T200_product_photo] PRIMARY KEY CLUSTERED 
(
	[ProductNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SysNo] [varchar](100) NOT NULL,
	[ProductNo] [varchar](100) NOT NULL,
	[ProductName] [varchar](200) NOT NULL,
	[TypeNo] [varchar](40) NOT NULL,
	[TypeName] [varchar](40) NOT NULL,
	[ProductPP] [varchar](100) NOT NULL,
	[ProductXH] [varchar](100) NOT NULL,
	[ProductCZ] [varchar](100) NOT NULL,
	[ProductHB] [varchar](100) NOT NULL,
	[ProductHD] [varchar](100) NOT NULL,
	[ProductGY] [varchar](100) NOT NULL,
	[ProductHS] [varchar](100) NOT NULL,
	[ProductMC] [varchar](100) NOT NULL,
	[ProductDJ] [varchar](100) NOT NULL,
	[ProductCD] [varchar](100) NOT NULL,
	[ProductGG] [varchar](100) NOT NULL,
	[ProductYS] [varchar](100) NOT NULL,
	[UnitNo] [varchar](100) NOT NULL,
	[UnitName] [varchar](100) NOT NULL,
	[ProductNote] [varchar](500) NOT NULL,
	[ProductBZGG] [varchar](100) NOT NULL,
	[BelongTypeNo] [varchar](40) NOT NULL,
	[BelongTypeName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_T200_product_code] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC,
	[ProductNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductSaleAreaDiffs]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductSaleAreaDiffs](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SysNo] [varchar](100) NOT NULL,
	[ProductNo] [varchar](100) NOT NULL,
	[StockNo] [varchar](40) NOT NULL,
	[AreaNo] [varchar](40) NOT NULL,
	[FirstAreaNo] [varchar](40) NOT NULL,
	[DiffPrice] [float] NOT NULL,
 CONSTRAINT [PK_T201_product_sale_area_diff] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC,
	[ProductNo] ASC,
	[StockNo] ASC,
	[AreaNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductSales]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductSales](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SysNo] [varchar](100) NOT NULL,
	[ProductNo] [varchar](40) NOT NULL,
	[StockNo] [varchar](40) NULL,
	[SalePrice] [float] NOT NULL,
 CONSTRAINT [PK_T201_product_sale_list] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC,
	[ProductNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SaleOrderDetails]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SaleOrderDetails](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SaleOrderGuid] [varchar](40) NOT NULL,
	[SaleOrderNo] [varchar](40) NOT NULL,
	[ProductNo] [varchar](40) NOT NULL,
	[ProductName] [varchar](40) NULL,
	[ProductPhotoUrl] [varchar](200) NULL,
	[CustomerNo] [varchar](40) NOT NULL,
	[InputDate] [datetime] NOT NULL,
	[OrderNum] [int] NOT NULL,
	[BasePrice] [float] NOT NULL,
	[DiffPrice] [float] NOT NULL,
	[SalePrice] [float] NOT NULL,
 CONSTRAINT [PK_SaleOrderDetails] PRIMARY KEY CLUSTERED 
(
	[SaleOrderGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SaleOrderMasters]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SaleOrderMasters](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SaleOrderNo] [varchar](40) NOT NULL,
	[CustomerNo] [varchar](40) NOT NULL,
	[InvoiceNo] [varchar](40) NOT NULL,
	[InputDate] [datetime] NOT NULL,
	[StockNo] [varchar](40) NOT NULL,
	[EditUserNo] [varchar](40) NOT NULL,
	[DeliveryDate] [datetime] NOT NULL,
	[Remark] [varchar](50) NULL,
 CONSTRAINT [PK_SaleOrderMasters] PRIMARY KEY CLUSTERED 
(
	[SaleOrderNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SaleOrderProgresses]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SaleOrderProgresses](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[SaleOrderNo] [varchar](40) NOT NULL,
	[ProgressGuid] [varchar](40) NOT NULL,
	[StepSn] [int] NOT NULL,
	[StepName] [varchar](100) NOT NULL,
	[StepTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SaleOrderProgresses] PRIMARY KEY CLUSTERED 
(
	[SaleOrderNo] ASC,
	[ProgressGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShoppingCarts]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShoppingCarts](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[CartGuid] [varchar](40) NOT NULL,
	[CustomerNo] [varchar](40) NOT NULL,
	[ProductNo] [varchar](40) NOT NULL,
	[ProductNum] [int] NOT NULL,
	[CartSelected] [bit] NOT NULL,
 CONSTRAINT [PK_T200_shopping_cart] PRIMARY KEY CLUSTERED 
(
	[CartGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Stocks]    Script Date: 2020/7/14 23:05:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stocks](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[StockNo] [varchar](40) NOT NULL,
	[StockName] [varchar](100) NOT NULL,
	[StockLinkman] [varchar](40) NOT NULL,
	[StockTel] [varchar](40) NOT NULL,
	[StockPhone] [varchar](40) NOT NULL,
 CONSTRAINT [PK_T200_stock_code] PRIMARY KEY CLUSTERED 
(
	[StockNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
