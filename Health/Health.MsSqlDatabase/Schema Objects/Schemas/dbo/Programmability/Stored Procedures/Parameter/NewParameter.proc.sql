CREATE PROCEDURE [dbo].[NewParameter]
	@nameParameter nvarchar(max), 
	@defaultValue varbinary(1)
AS
	declare @status int =1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	
	begin try
		insert into Parameters(Name, DefaultValue) values(@nameParameter, @defaultValue)
	end try
		
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage

RETURN 0