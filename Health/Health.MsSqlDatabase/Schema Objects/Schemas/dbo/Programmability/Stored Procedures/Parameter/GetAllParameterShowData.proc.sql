CREATE PROCEDURE [dbo].[GetAllParameterShowData]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		select
		pa.ParameterId as ParameterId,
		pa.ParameterId as Id,
		pa.Name as Name,
		pa.DefaultValue as DefaultValue,
		@status as Status, @statusMessage as StatusMessage
		from Parameters as pa
			
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage

RETURN 0