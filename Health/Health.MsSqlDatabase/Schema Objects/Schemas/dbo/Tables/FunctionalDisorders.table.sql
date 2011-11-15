CREATE TABLE [dbo].[FunctionalDisorders]
(
	FunctionalDisordersId int NOT NULL IDENTITY(1, 1),
	Name nvarchar(MAX) NOT NULL, 
	Parent int NULL
)
