/****** Object:  Table [dbo].[FilterRecord]    Script Date: 06/12/2016 11:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FilterRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_dbo.FilterRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ImageRecord]    Script Date: 06/12/2016 11:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](max) NULL,
	[FilterId] [int] NOT NULL,
	[FilterRecord_Id] [int] NULL,
 CONSTRAINT [PK_dbo.ImageRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ImageRecord]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ImageRecord_dbo.FilterRecord_FilterId] FOREIGN KEY([FilterId])
REFERENCES [dbo].[FilterRecord] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImageRecord] CHECK CONSTRAINT [FK_dbo.ImageRecord_dbo.FilterRecord_FilterId]
GO
ALTER TABLE [dbo].[ImageRecord]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ImageRecord_dbo.FilterRecord_FilterRecord_Id] FOREIGN KEY([FilterRecord_Id])
REFERENCES [dbo].[FilterRecord] ([Id])
GO
ALTER TABLE [dbo].[ImageRecord] CHECK CONSTRAINT [FK_dbo.ImageRecord_dbo.FilterRecord_FilterRecord_Id]
GO
