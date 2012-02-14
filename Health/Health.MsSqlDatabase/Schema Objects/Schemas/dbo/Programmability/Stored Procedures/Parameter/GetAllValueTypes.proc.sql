CREATE PROCEDURE [dbo].[GetAllValueTypes]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		select @status as Status, @statusMessage as StatusMessage,
			v.ValueTypeId,
			v.Name,
			'0' as Id
			from ValueTypes as v
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0