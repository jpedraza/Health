CREATE PROCEDURE [dbo].[UpdateParameter]
	@ParameterId int, 
	@Name nvarchar(MAX),
	@DefaultValue varbinary(MAX)
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		update dbo.Parameters
			set Name=@Name,
				DefaultValue=@DefaultValue
			where ParameterId=@ParameterId
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0