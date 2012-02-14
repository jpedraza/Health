CREATE PROCEDURE [dbo].[UpdateParameterMetadata]
	@ParameterId int, 
	@Key nvarchar(MAX),
	@Value varbinary(MAX),
	@ValueTypeId int
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		update dbo.ParameterMetadata
			set [Key]=@Key,
				Value=@Value,
				ValueTypeId=@ValueTypeId
			where ParameterId=@ParameterId
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0