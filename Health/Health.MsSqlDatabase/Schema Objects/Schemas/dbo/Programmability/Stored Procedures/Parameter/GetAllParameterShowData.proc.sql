CREATE PROCEDURE [dbo].[GetAllParameterShowData]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	select
	pa.ParameterId,
	Name,
	@status as Status, @statusMessage as StatusMessage
	from Parameters as pa

RETURN 0