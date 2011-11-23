USE [Health.MsSqlDatabase]
GO
/****** Object:  Trigger [dbo].[InsertWorkDayOfDoctors]    Script Date: 11/23/2011 10:39:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[InsertWorkDayOfDoctors]
ON [dbo].[WorkWeeks]
AFTER INSERT 
AS 
BEGIN	
	DECLARE @PEREM INT
	SET @PEREM = (SELECT TOP 1 inserted.DayInWeek FROM inserted)
	PRINT @PEREM
	IF EXISTS (SELECT*FROM [WorkWeeks] 
	WHERE (SELECT DayInWeek FROM inserted) = WorkWeeks.DayInWeek
	AND
	(SELECT DoctorId FROM inserted) = WorkWeeks.DoctorId
	)
	BEGIN
	ROLLBACK TRAN
	PRINT 'ƒл€ данного дн€ и доктора уже существует расписание'
	END
END