CREATE TABLE [dbo].[WorkWeeks]
(
	DoctorId int NOT NULL,
	DayInWeek int NOT NULL,
	TimeStart Time NOT NULL,
	TimeEnd Time NOT NULL,
	DinnerStart Time NOT NULL,
	DinnerEnd Time NOT NULL,
	AttendingHoursStart Time NOT NULL,
	AttendingHoursEnd Time NOT NULL,
	AttendingMinutes int NOT NULL
)
