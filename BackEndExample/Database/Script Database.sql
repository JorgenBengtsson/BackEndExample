/*** Create a database called BackEndExample and run this script in that database ***/

USE [BackEndExample]
GO
/****** Object:  Table [dbo].[ExampleData1]    Script Date: 2019-09-27 14:03:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExampleData1](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Column1] [varchar](50) NULL,
	[Column2] [int] NULL,
	[Column3] [datetime] NOT NULL,
 CONSTRAINT [PK_ExampleData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExampleData2]    Script Date: 2019-09-27 14:03:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExampleData2](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ExampleDataid] [int] NOT NULL,
	[Column4] [varchar](50) NULL,
	[Column5] [date] NULL,
 CONSTRAINT [PK_ExampleData2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ExampleData1] ON 
GO
INSERT [dbo].[ExampleData1] ([id], [Column1], [Column2], [Column3]) VALUES (1, N'Data1', 1, CAST(N'2019-09-27T13:54:47.127' AS DateTime))
GO
INSERT [dbo].[ExampleData1] ([id], [Column1], [Column2], [Column3]) VALUES (2, N'Data2', 2, CAST(N'2019-09-27T13:55:03.940' AS DateTime))
GO
INSERT [dbo].[ExampleData1] ([id], [Column1], [Column2], [Column3]) VALUES (3, N'Data3', 3, CAST(N'2019-09-27T13:55:06.740' AS DateTime))
GO
INSERT [dbo].[ExampleData1] ([id], [Column1], [Column2], [Column3]) VALUES (4, N'Data4', 4, CAST(N'2019-09-27T13:55:09.777' AS DateTime))
GO
INSERT [dbo].[ExampleData1] ([id], [Column1], [Column2], [Column3]) VALUES (5, N'Data5', 5, CAST(N'2019-09-27T13:55:13.780' AS DateTime))
GO
INSERT [dbo].[ExampleData1] ([id], [Column1], [Column2], [Column3]) VALUES (6, N'Data6', 6, CAST(N'2019-09-27T13:55:24.677' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ExampleData1] OFF
GO
SET IDENTITY_INSERT [dbo].[ExampleData2] ON 
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (1, 1, N'Data11', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (2, 2, N'Data22', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (3, 3, N'Data33', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (4, 4, N'Data44', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (5, 5, N'Data55', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (6, 1, N'Data61', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (7, 1, N'Data71', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (8, 2, N'Data82', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (9, 3, N'Data93', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (10, 3, N'DataA3', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (11, 3, N'DataB3', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (12, 3, N'DataC3', CAST(N'2019-09-27' AS Date))
GO
INSERT [dbo].[ExampleData2] ([id], [ExampleDataid], [Column4], [Column5]) VALUES (13, 3, N'DataD3', CAST(N'2019-09-27' AS Date))
GO
SET IDENTITY_INSERT [dbo].[ExampleData2] OFF
GO
ALTER TABLE [dbo].[ExampleData1] ADD  CONSTRAINT [DF_Table_1_Kolumn3]  DEFAULT (getdate()) FOR [Column3]
GO
ALTER TABLE [dbo].[ExampleData2] ADD  CONSTRAINT [DF_ExampleData2_Column5]  DEFAULT (getdate()) FOR [Column5]
GO
