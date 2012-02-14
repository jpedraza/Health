CREATE PROCEDURE [dbo].[UpdateValueTypes]
	@ValueTypeId int = 0, 
	@Name nvarchar(MAX)
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		update dbo.ValueTypes
			set Name=@Name
			where ValueTypeId=@ValueTypeId
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0