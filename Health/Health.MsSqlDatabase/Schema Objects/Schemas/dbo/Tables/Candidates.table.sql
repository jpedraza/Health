CREATE TABLE [dbo].[Candidates]
(
	CandidateId int NOT NULL IDENTITY(1, 1),
	FirstName nvarchar(MAX) NOT NULL,
    LastName nvarchar(MAX) NOT NULL,
    ThirdName nvarchar(MAX) NOT NULL,
    Login nvarchar(MAX) NOT NULL,
    Password nvarchar(MAX) NOT NULL,
    RoleId int NOT NULL,
    Birthday datetime NOT NULL,
    Token nvarchar(MAX) NOT NULL,
	Policy nvarchar(MAX) NOT NULL,
	Card nvarchar(MAX) NOT NULL
)
