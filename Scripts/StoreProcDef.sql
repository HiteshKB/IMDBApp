USE [My_IMDB]
GO

/****** Object:  StoredProcedure [dbo].[GetActors]    Script Date: 13-08-2018 01:37:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetActors]
AS
BEGIN
	select * from Actors
END
GO


CREATE PROCEDURE [dbo].[GetMoviesData]
AS
BEGIN
	select m.MovieID, m.Name as MovieName, m.Year as ReleaseYear, m.Plot as Genre, m.Poster,p.Name as Producer, dbo.GetActorsList(m.MovieID) as Actors
	from Movies m, Producers p 
	where m.ProducerID=p.ProducerID
END
GO


CREATE PROCEDURE [dbo].[GetProducers]
AS
BEGIN
	select * from Producers
END
GO


CREATE PROCEDURE [dbo].[spGetActorList]
AS
BEGIN
	select ActorID, Name from Actors
END
GO


CREATE PROCEDURE [dbo].[spGetProducersList]
AS
BEGIN
	select ProducerID, Name from Producers
END
GO


CREATE PROCEDURE [dbo].[spInsertActor]
@name nvarchar(50),
@sex nvarchar(1),
@dob nvarchar(10),
@bio nvarchar(100)
AS
BEGIN
	INSERT INTO [dbo].[Actors] ([Name],[Gender],[DOB],[BIO]) VALUES(@name,@sex,@dob,@bio)
	return @@ROWCOUNT
END
GO


CREATE PROCEDURE [dbo].[spInsertMovie]
@name nvarchar(50),
@year smallint,
@plot nvarchar(20),
@poster nvarchar(50),
@prdcrID smallint,
@movieID smallint out
AS
BEGIN
	INSERT INTO [dbo].[Movies]([Name],[Year],[Plot],[Poster],[ProducerID])
	VALUES(@name,@year,@plot,@poster,@prdcrID)
	select @movieID=MovieID from Movies where Name=@name and ProducerID=@prdcrID
	return @@ROWCOUNT
END
GO


CREATE PROCEDURE [dbo].[spInsertProducer]
@name nvarchar(50),
@sex nvarchar(1),
@dob nvarchar(10),
@bio nvarchar(100)
AS
BEGIN
	INSERT INTO [dbo].[Producers] ([Name],[Gender],[DOB],[BIO]) VALUES(@name,@sex,@dob,@bio)
	return @@ROWCOUNT
END
GO

----------------------------------------------------------------------------------------------
--Funtions
----------------------------------------------------------------------------------------------

CREATE FUNCTION [dbo].[GetActorsList]      
(
	-- Add the parameters for the function here
	@movieID smallInt
)
RETURNS varchar(1000)
AS
BEGIN
	DECLARE @result varchar(1000)
	SET @result = ''
	SELECT @result = @result + a.Name + ',' 
	FROM Actors a
	WHERE a.ActorID IN (select map.ActorID from MovieStarMap map where map.MovieID=@movieID)
	-- Return the result of the function
	RETURN substring(@result, 0, len(@result))

END
GO