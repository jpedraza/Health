-- Персональные расписания
-- [class PersonalSchedule]
CREATE TABLE [dbo].[PersonalSchedule]
(
	PersonalScheduleId int NOT NULL IDENTITY(1, 1),
	PatientId int, -- идентификатор пользователя
	ParameterId int, -- идентификатор параметра

	-- Период в течении которого отслеживается параметр
		DateStart DateTime, -- начало периода
		DateEnd DateTime, -- конец периода
	--//

	-- Промежуток времени, в течении которого возможен ввод параметра
		TimeStart Time, -- начало промежутка
		TimeEnd Time, -- конец промежутка
	--//

	-- Характеристика дня в который возможен ввод параметра
		-- [class Day]
		DayOfWeek int, 
		DayOfMonth int,

		-- [class Month]
		MonthOfYear int,

		-- [class Week]
		WeekOfMonth int,
		WeekParity int,
	--//
)
