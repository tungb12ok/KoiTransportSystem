USE [KoiFishTransportationDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/13/2024 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMS_Content]    Script Date: 11/13/2024 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMS_Content](
	[content_id] [int] IDENTITY(1,1) NOT NULL,
	[create_by] [int] NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[content_type] [nvarchar](50) NULL,
	[image] [nvarchar](max) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK_CMS_Content] PRIMARY KEY CLUSTERED 
(
	[content_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 11/13/2024 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[feedback_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NULL,
	[customer_id] [int] NULL,
	[rating] [int] NOT NULL,
	[comments] [nvarchar](max) NULL,
	[feedback_date] [datetime] NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[feedback_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Koi_Orders]    Script Date: 11/13/2024 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Koi_Orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[pickup_location] [nvarchar](255) NOT NULL,
	[destination] [nvarchar](255) NOT NULL,
	[weight] [decimal](10, 2) NOT NULL,
	[quantity] [int] NOT NULL,
	[transport_method] [nvarchar](255) NOT NULL,
	[total] [money] NULL,
	[additional_services] [nvarchar](max) NULL,
	[pricing_id] [int] NULL,
	[status] [nvarchar](50) NULL,
	[placed_date] [datetime] NULL,
	[completed_date] [datetime] NULL,
 CONSTRAINT [PK_Koi_Orders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Documents]    Script Date: 11/13/2024 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Documents](
	[document_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NULL,
	[document_type] [nvarchar](50) NOT NULL,
	[document_path] [nvarchar](255) NOT NULL,
	[created_at] [datetime] NULL,
	[image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Order_Documents] PRIMARY KEY CLUSTERED 
(
	[document_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pricing]    Script Date: 11/13/2024 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pricing](
	[price_id] [int] IDENTITY(1,1) NOT NULL,
	[transport_method] [nvarchar](255) NOT NULL,
	[weight_range] [nvarchar](255) NOT NULL,
	[price_per_kg] [decimal](10, 2) NOT NULL,
	[additional_service_price] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Pricing] PRIMARY KEY CLUSTERED 
(
	[price_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/13/2024 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transport_Process]    Script Date: 11/13/2024 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transport_Process](
	[process_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NULL,
	[step] [nvarchar](255) NOT NULL,
	[status] [nvarchar](50) NULL,
	[process_date] [datetime] NULL,
 CONSTRAINT [PK_Transport_Process] PRIMARY KEY CLUSTERED 
(
	[process_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/13/2024 10:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[password_hash] [nvarchar](255) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[phone] [nvarchar](20) NOT NULL,
	[address] [nvarchar](255) NOT NULL,
	[role_id] [int] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241030174403_init', N'6.0.27')
GO
SET IDENTITY_INSERT [dbo].[CMS_Content] ON 

INSERT [dbo].[CMS_Content] ([content_id], [create_by], [title], [content], [content_type], [image], [created_at], [updated_at]) VALUES (1, 6, N'Koi', N'Koi system
', N'Store', N'https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcTNGC9HP0kvPVjRmDosz7w_OwVSLdTEhI0ISVS4U8jsc8BDkahQ', CAST(N'2024-10-30T17:59:27.607' AS DateTime), NULL)
INSERT [dbo].[CMS_Content] ([content_id], [create_by], [title], [content], [content_type], [image], [created_at], [updated_at]) VALUES (2, 6, N'Koi 2', N'Koi 2', N'Koi 2', N'https://th.bing.com/th?id=OSK.HEROMKryA1-hTe2Egs6XmDlNULaCP92i2jP0aNX4k5AFlAw&w=472&h=280&c=13&rs=2&o=6&oif=webp&dpr=1.3&pid=SANGAM', CAST(N'2024-10-31T06:04:56.657' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[CMS_Content] OFF
GO
SET IDENTITY_INSERT [dbo].[Koi_Orders] ON 

INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (1, 8, N'saigon', N'hanoi', CAST(1.00 AS Decimal(10, 2)), 2, N'Air', 200000.0000, N'', 1, N'Delivered', CAST(N'2024-10-30T18:07:26.490' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (2, 1, N'saigon', N'hanoi', CAST(12.00 AS Decimal(10, 2)), 2, N'Air', 2400000.0000, N'aaaa', 1, N'Delivered', CAST(N'2024-10-30T18:14:15.187' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (3, 1, N'saigon', N'hanoi', CAST(312.00 AS Decimal(10, 2)), 2, N'Air', 62400000.0000, N'', 1, N'Delivered', CAST(N'2024-10-30T18:15:54.980' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4, 1, N'saigon', N'hanoi', CAST(122.00 AS Decimal(10, 2)), 1, N'Air', 1220000.0000, N'1', 4, N'Delivered', CAST(N'2024-10-30T18:17:17.133' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (1002, 1, N'hcm', N'hn', CAST(5.00 AS Decimal(10, 2)), 2, N'Air', 2500000.0000, N'', 3, N'Canceled', CAST(N'2024-10-31T05:41:22.680' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (1003, 1, N'hcm', N'hn', CAST(3.00 AS Decimal(10, 2)), 3, N'Sea', 10000.0000, N'', 1, N'Delivered', CAST(N'2024-10-31T05:48:16.203' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (1004, 1, N'gh', N'tr', CAST(3.00 AS Decimal(10, 2)), 4, N'Sea', 30000.0000, N'w', 1003, N'Delivered', CAST(N'2024-10-31T05:51:50.933' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (1005, 1, N'hcm', N'hn', CAST(20.00 AS Decimal(10, 2)), 2, N'Air', 200000.0000, N'cá đỏ', 4, N'Delivered', CAST(N'2024-10-31T08:21:21.187' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (1006, 1, N'fsd', N'fd', CAST(1.00 AS Decimal(10, 2)), 1, N'Air', 10000.0000, N'', 1, N'Delivered', CAST(N'2024-10-31T08:40:45.037' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (1007, 1, N'hvn', N'we', CAST(2.00 AS Decimal(10, 2)), 2, N'Air', 40000.0000, N'', 2, N'Delivered', CAST(N'2024-10-31T08:42:46.610' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (2002, 1, N'HN', N'sai gon', CAST(12.00 AS Decimal(10, 2)), 22, N'Land', 240000.0000, N'', 2, N'Delivered', CAST(N'2024-11-01T15:05:30.623' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (3002, 1, N'hmc', N'hn', CAST(10.00 AS Decimal(10, 2)), 2, N'', 200000.0000, N'nho3', 2, N'Delivered', CAST(N'2024-11-02T06:45:28.967' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (3003, 1, N'hcm', N'hn', CAST(12.00 AS Decimal(10, 2)), 2, N'', 6000000.0000, N'', 3, N'Delivered', CAST(N'2024-11-02T06:46:52.400' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (3004, 1, N'hcm', N'hn', CAST(20.00 AS Decimal(10, 2)), 4, N'', 10000000.0000, N'', 3, N'Delivered', CAST(N'2024-11-02T06:49:04.227' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4002, 1, N'hcm', N'hn', CAST(20.00 AS Decimal(10, 2)), 2, N'', 400000.0000, N'', 2, N'Delivered', CAST(N'2024-11-10T04:54:53.273' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4003, 1, N'dn', N'hn', CAST(10.00 AS Decimal(10, 2)), 3, N'', 100000.0000, N'', 1003, N'Canceled', CAST(N'2024-11-10T04:57:35.620' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4004, 1, N'dn', N'hn', CAST(4.00 AS Decimal(10, 2)), 2, N'', 2000000.0000, N'', 3, N'Pending', CAST(N'2024-11-10T05:05:44.810' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4005, 1, N'a', N'a', CAST(12.00 AS Decimal(10, 2)), 0, N'', 27840000.0000, N'', 1, N'WaitingForPayment', CAST(N'2024-11-10T06:22:30.607' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4006, 1, N'1', N'1', CAST(12.00 AS Decimal(10, 2)), 0, N'', 27840000.0000, N'', 1, N'WaitingForPayment', CAST(N'2024-11-10T06:23:01.663' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4007, 1, N'1', N'1', CAST(1.00 AS Decimal(10, 2)), 1, N'', 2320000.0000, N'', 1, N'WaitingForPayment', CAST(N'2024-11-10T06:28:07.463' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4008, 1, N'q', N'q', CAST(11.00 AS Decimal(10, 2)), 1, N'', 1650000.0000, N'', 1, N'WaitingForPayment', CAST(N'2024-11-10T06:29:40.403' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4009, 1, N'dn', N'hn', CAST(4.00 AS Decimal(10, 2)), 3, N'', 600000.0000, N'', 1, N'WaitingForPayment', CAST(N'2024-11-10T06:30:36.770' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4010, 1, N'hn', N'hcm', CAST(6.00 AS Decimal(10, 2)), 1, N'', 900000.0000, N'', 1, N'WaitingForPayment', CAST(N'2024-11-10T06:33:34.880' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4011, 1, N'a', N'a', CAST(1.00 AS Decimal(10, 2)), 1, N'', 150000.0000, N'', 1, N'WaitingForPayment', CAST(N'2024-11-10T06:35:06.113' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (4012, 1, N'hcm', N'hn', CAST(1.00 AS Decimal(10, 2)), 2, N'', 150000.0000, N'', 1, N'WaitingForPayment', CAST(N'2024-11-10T06:36:08.353' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (5002, 1, N'HCM', N'HN', CAST(4.00 AS Decimal(10, 2)), 2, N'', 200000.0000, N'', 3, N'WaitingForPayment', CAST(N'2024-11-11T05:01:00.607' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (5003, 1, N'hcm', N'hn', CAST(5.00 AS Decimal(10, 2)), 2, N'', 250000.0000, N'', 3, N'WaitingForPayment', CAST(N'2024-11-11T05:03:11.600' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (5004, 1, N'dn', N'hcm', CAST(14.00 AS Decimal(10, 2)), 4, N'', 140000.0000, N'', 4, N'WaitingForPayment', CAST(N'2024-11-11T05:05:01.577' AS DateTime), NULL)
INSERT [dbo].[Koi_Orders] ([order_id], [customer_id], [pickup_location], [destination], [weight], [quantity], [transport_method], [total], [additional_services], [pricing_id], [status], [placed_date], [completed_date]) VALUES (6002, 1, N'hcm', N'hn', CAST(23.00 AS Decimal(10, 2)), 2, N'', 1150000.0000, N'', 3, N'WaitingForPayment', CAST(N'2024-11-13T13:30:30.290' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Koi_Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Order_Documents] ON 

INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1, 2, N'123', N'123', CAST(N'2024-10-31T05:44:47.510' AS DateTime), N'123')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (2, 4, N'123', N'', CAST(N'2024-10-31T05:46:33.140' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (3, 1004, N'', N'', CAST(N'2024-10-31T05:53:47.410' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1002, 3, N'', N'', CAST(N'2024-11-10T04:58:18.037' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1003, 1, N'', N'', CAST(N'2024-11-10T04:58:19.443' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1004, 4002, N'', N'', CAST(N'2024-11-10T04:59:46.320' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1005, 3004, N'', N'', CAST(N'2024-11-10T04:59:48.500' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1006, 3003, N'', N'', CAST(N'2024-11-10T04:59:49.603' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1007, 1007, N'', N'', CAST(N'2024-11-10T04:59:51.047' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1008, 1003, N'', N'', CAST(N'2024-11-10T04:59:52.600' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1009, 2002, N'', N'', CAST(N'2024-11-10T04:59:54.007' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1010, 1005, N'', N'', CAST(N'2024-11-10T04:59:56.780' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1011, 1006, N'', N'', CAST(N'2024-11-10T04:59:58.030' AS DateTime), N'')
INSERT [dbo].[Order_Documents] ([document_id], [order_id], [document_type], [document_path], [created_at], [image]) VALUES (1012, 3002, N'', N'', CAST(N'2024-11-10T04:59:59.733' AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[Order_Documents] OFF
GO
SET IDENTITY_INSERT [dbo].[Pricing] ON 

INSERT [dbo].[Pricing] ([price_id], [transport_method], [weight_range], [price_per_kg], [additional_service_price]) VALUES (1, N'Air', N'10-200kg', CAST(150000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Pricing] ([price_id], [transport_method], [weight_range], [price_per_kg], [additional_service_price]) VALUES (2, N'Ground', N'5-10kg', CAST(20000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Pricing] ([price_id], [transport_method], [weight_range], [price_per_kg], [additional_service_price]) VALUES (3, N'Air', N'0-5kg', CAST(50000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Pricing] ([price_id], [transport_method], [weight_range], [price_per_kg], [additional_service_price]) VALUES (4, N'Ground', N'5-10kg', CAST(10000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)))
INSERT [dbo].[Pricing] ([price_id], [transport_method], [weight_range], [price_per_kg], [additional_service_price]) VALUES (1003, N'Sea', N'0-5kg', CAST(10000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Pricing] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (1, N'Customer')
INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (2, N'Manager')
INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (3, N'SalesStaff')
INSERT [dbo].[Roles] ([role_id], [role_name]) VALUES (4, N'DeliveringStaff')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([user_id], [email], [password_hash], [name], [phone], [address], [role_id], [created_at]) VALUES (1, N'tam1@gmail.com', N'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=', N'tam1', N'11111', N'11111', 1, CAST(N'2024-10-30T17:50:28.567' AS DateTime))
INSERT [dbo].[Users] ([user_id], [email], [password_hash], [name], [phone], [address], [role_id], [created_at]) VALUES (6, N'tam2@gmail.com', N'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=', N'a', N'a', N'a', 2, CAST(N'2024-10-30T17:51:31.450' AS DateTime))
INSERT [dbo].[Users] ([user_id], [email], [password_hash], [name], [phone], [address], [role_id], [created_at]) VALUES (7, N'tam3@gmail.com', N'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=', N'a', N'a', N'a', 3, CAST(N'2024-10-30T17:52:00.820' AS DateTime))
INSERT [dbo].[Users] ([user_id], [email], [password_hash], [name], [phone], [address], [role_id], [created_at]) VALUES (8, N'tam4@gmail.com', N'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=', N'a', N'a', N'a', 4, CAST(N'2024-10-30T17:52:18.393' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[CMS_Content] ADD  CONSTRAINT [DF__CMS_Conte__creat__534D60F1]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Feedback] ADD  DEFAULT (getdate()) FOR [feedback_date]
GO
ALTER TABLE [dbo].[Koi_Orders] ADD  DEFAULT ('Pending') FOR [status]
GO
ALTER TABLE [dbo].[Koi_Orders] ADD  DEFAULT (getdate()) FOR [placed_date]
GO
ALTER TABLE [dbo].[Order_Documents] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Transport_Process] ADD  DEFAULT ('Pending') FOR [status]
GO
ALTER TABLE [dbo].[Transport_Process] ADD  DEFAULT (getdate()) FOR [process_date]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[CMS_Content]  WITH CHECK ADD  CONSTRAINT [FK_CMS_Content_Users] FOREIGN KEY([create_by])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[CMS_Content] CHECK CONSTRAINT [FK_CMS_Content_Users]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[Koi_Orders] ([order_id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Orders]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Users] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Users]
GO
ALTER TABLE [dbo].[Koi_Orders]  WITH CHECK ADD  CONSTRAINT [FK_Koi_Orders_Pricing] FOREIGN KEY([pricing_id])
REFERENCES [dbo].[Pricing] ([price_id])
GO
ALTER TABLE [dbo].[Koi_Orders] CHECK CONSTRAINT [FK_Koi_Orders_Pricing]
GO
ALTER TABLE [dbo].[Koi_Orders]  WITH CHECK ADD  CONSTRAINT [FK_Koi_Orders_Users] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Koi_Orders] CHECK CONSTRAINT [FK_Koi_Orders_Users]
GO
ALTER TABLE [dbo].[Order_Documents]  WITH CHECK ADD  CONSTRAINT [FK_Order_Documents_Orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[Koi_Orders] ([order_id])
GO
ALTER TABLE [dbo].[Order_Documents] CHECK CONSTRAINT [FK_Order_Documents_Orders]
GO
ALTER TABLE [dbo].[Transport_Process]  WITH CHECK ADD  CONSTRAINT [FK_Transport_Process_Orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[Koi_Orders] ([order_id])
GO
ALTER TABLE [dbo].[Transport_Process] CHECK CONSTRAINT [FK_Transport_Process_Orders]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([role_id])
REFERENCES [dbo].[Roles] ([role_id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
