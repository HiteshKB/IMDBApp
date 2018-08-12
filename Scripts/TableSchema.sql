USE [My_IMDB]
GO

/****** Object:  Table [dbo].[Producers]    Script Date: 11-08-2018 17:04:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Producers](
	[ProducerID] [smallint] IDENTITY(100,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](1) NOT NULL,
	[DOB] [nvarchar](20) NULL,
	[BIO] [nvarchar](100) NULL,
 CONSTRAINT [PK_Producers] PRIMARY KEY CLUSTERED 
(
	[ProducerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Producers]  WITH CHECK ADD CHECK  (([Gender]='M' OR [Gender]='F'))
GO

CREATE TABLE [dbo].[Actors](
	[ActorID] [smallint] IDENTITY(200,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](1) NOT NULL,
	[DOB] [nvarchar](20) NULL,
	[BIO] [nvarchar](100) NULL,
 CONSTRAINT [PK_Actors] PRIMARY KEY CLUSTERED 
(
	[ActorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Actors]  WITH CHECK ADD CHECK  (([Gender]='M' OR [Gender]='F'))
GO

CREATE TABLE [dbo].[Movies](
	[MovieID] [smallint] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Year] [smallint] NOT NULL,
	[Plot] [nvarchar](20) NULL,
	[Poster] [nvarchar](100) NULL,
	[ProducerID] [smallint] NOT NULL,
 CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Movies]  WITH CHECK ADD  CONSTRAINT [FK_Movies_Producers] FOREIGN KEY([ProducerID])
REFERENCES [dbo].[Producers] ([ProducerID])
GO

ALTER TABLE [dbo].[Movies] CHECK CONSTRAINT [FK_Movies_Producers]
GO

CREATE TABLE [dbo].[MovieStarMap](
	[MovieID] [smallint] NOT NULL,
	[ActorID] [smallint] NOT NULL,
	CONSTRAINT CompositeKeyMapping PRIMARY KEY ([MovieID], [ActorID])
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MovieStarMap]  WITH CHECK ADD  CONSTRAINT [FK_MovieStarMap_Actors] FOREIGN KEY([ActorID])
REFERENCES [dbo].[Actors] ([ActorID])
GO

ALTER TABLE [dbo].[MovieStarMap] CHECK CONSTRAINT [FK_MovieStarMap_Actors]
GO

ALTER TABLE [dbo].[MovieStarMap]  WITH CHECK ADD  CONSTRAINT [FK_MovieStarMap_Movies] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movies] ([MovieID])
GO

ALTER TABLE [dbo].[MovieStarMap] CHECK CONSTRAINT [FK_MovieStarMap_Movies]
GO

