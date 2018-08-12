INSERT INTO [dbo].[Actors] ([Name],[Gender],[DOB],[BIO]) VALUES('Hitesh','M','26/09/1993','Perfectionist')
INSERT INTO [dbo].[Actors] ([Name],[Gender],[DOB],[BIO]) VALUES('Ritesh','M','26/09/1994','Comedy')
INSERT INTO [dbo].[Actors] ([Name],[Gender],[DOB],[BIO]) VALUES('Jitesh','M','26/09/1995','Thriller')

INSERT INTO [dbo].[Producers] ([Name],[Gender],[DOB],[BIO]) VALUES('Mahesh','M','26/09/1993','Perfectionist')
INSERT INTO [dbo].[Producers] ([Name],[Gender],[DOB],[BIO]) VALUES('Ramesh','M','26/09/1994','Comedy')
INSERT INTO [dbo].[Producers] ([Name],[Gender],[DOB],[BIO]) VALUES('Mukesh','M','26/09/1995','Thriller')

INSERT INTO [dbo].[Movies]([Name],[Year],[Plot],[Poster],[ProducerID])VALUES('3 Idiots','2014','Comedy','3iddiots.png',101)
INSERT INTO [dbo].[Movies]([Name],[Year],[Plot],[Poster],[ProducerID])VALUES('3 Idiots','2013','Perfect','3iddiots.png',100)
INSERT INTO [dbo].[Movies]([Name],[Year],[Plot],[Poster],[ProducerID])VALUES('Soorma','2014','Sports','Soorma.png',102)

INSERT INTO [dbo].[MovieStarMap]([MovieID],[ActorID]) values (1002,200)
INSERT INTO [dbo].[MovieStarMap]([MovieID],[ActorID]) values (1002,201)
INSERT INTO [dbo].[MovieStarMap]([MovieID],[ActorID]) values (1002,202)
INSERT INTO [dbo].[MovieStarMap]([MovieID],[ActorID]) values (1003,201)
INSERT INTO [dbo].[MovieStarMap]([MovieID],[ActorID]) values (1004,200)
INSERT INTO [dbo].[MovieStarMap]([MovieID],[ActorID]) values (1004,203)

