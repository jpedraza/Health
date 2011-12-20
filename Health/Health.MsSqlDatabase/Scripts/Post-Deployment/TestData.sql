/*
Шаблон скрипта после развертывания							
------------------------------------------------------------------------------------//
 В данном файле содержатся операторы SQL, которые будут добавлены в скрипт построения.		
 Используйте синтаксис SQLCMD для включения файла в скрипт после развертывания.			
 Пример:      :r .\myfile.sql								
 Используйте синтаксис SQLCMD для создания ссылки на переменную в скрипте после развертывания.		
 Пример:      :setvar TableName MyTable							
			   SELECT * FROM [$(TableName)]					
------------------------------------------------------------------------------------//
*/
USE [Health.MsSqlDatabase]
/*Заполняем роли*/
insert into Roles (Name) values('Пациент')
insert into Roles (Name) values('Доктор')
insert into Roles (Name) values('Администратор')

/*заполняем пользователей сайта*/
insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Селигеров', 'Павел', 'Васильевич', 'логин', 'пароль', 1, '1991/12/04', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Швецова', 'Мария', 'Сергеевна', 'логин', 'пароль', 1, '1991/11/11', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Иванов', 'Сергей', 'Иванович', 'логин', 'пароль', 1, '1992/07/10', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Васнецов', 'Иван', 'Львович', 'логин', 'пароль', 1, '2001/04/03', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Иванов', 'Иван', 'Степанович', 'логин', 'пароль', 1, '1999/12/04', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Гаус', 'Никита', 'Анисимович', 'логин', 'пароль', 1, '2002/12/09', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Сухарев', 'Иван', 'Дмитриевич', 'логин', 'пароль', 1, '2003/12/11', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Иванов', 'Олег', 'Юрьевич', 'логин', 'пароль', 1, '2000/02/08', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Старосельцев', 'Олег', 'Александрович', 'логин', 'пароль', 1, '1997/11/01', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Энштейн', 'Альберт', 'Петрович', 'логин', 'пароль', 1, '1991/12/10', 'token_value')

/*Заполняем работников: */
insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Энштейн', 'Альберт', 'Витальевич', 'админ', 'фдмин', 3, '1975/12/10', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Дроздов', 'Евгений', 'Павлович', 'логин', 'пароль', 2, '1956/10/08', 'token_value')

/*Заполняем специальности*/
insert into Specialties(Name) values('Врач/кардиолог')
insert into Specialties(Name) values('Врач/терапевт')

/*Заполняем докторов*/
insert into Doctors(DoctorId, SpecialtyId) values(12, 1)

/*Заполняем функциональные классы*/
insert into FunctionalClasses(Code, Description)
	 values('Функциональный класс I', 'пациенты с заболеванием сердца,
	  у которых обычные физические нагрузки не вызывают одышки, утомления или сердцебиения.')

insert into FunctionalClasses(Code, Description)
	 values('Функциональный класс II', 'пациенты с заболеванием сердца и умеренным ограничением
	  физической активности. При обычных физических нагрузках наблюдаются одышка, усталость и сердцебиение.')

insert into FunctionalClasses(Code, Description)
	 values('Функциональный класс III', 'пациенты с заболеванием сердца и выраженным ограничением
	  физической активности. В состоянии покоя жалобы отсутствуют, но даже при незначительных физических
	   нагрузках появляются одышка, усталость и сердцебиение.')

insert into FunctionalClasses(Code, Description)
	 values('Функциональный класс IV', 'пациенты с заболеванием сердца, у которых любой
	  уровень физической активности вызывает указанные выше субъективные симптомы.
	   Последние возникают и в состоянии покоя.')

/*Заполняем пациентов*/
insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 3, 'Анастасия Сергеевна', 1, 34435, 32333, '78935632', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 2, 'Евгения Павловна', 2, 34435, 32333, '64273642', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 1, 'Лидия Петровна', 3, 34435, 32333, '65426953', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 1, 'Алла Васильевна', 4, 34435, 32333, '5467253', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 4, 'Юлия Владимировна', 5, 34435, 32333, '5465421', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 3, 'Александра Дмитриевна', 6, 34435, 32333, '87684213', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 2, 'Светлана Тимофеевна', 7, 34435, 32333, '42362389', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 1, 'Анна Игоревна', 8, 34435, 32333, '5234423', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 1, 'Татьяна Петровна', 9, 34435, 32333, '2346274', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 1, 'Марья Ивановна', 10, 34435, 32333, '453259482', '2001/09/21')

/*Заполняем виды операций*/
insert Surgerys(Name, SurgeryDescription) values
('Surgery1',
'Пластика ДМЖП по методике двойной заплаты в условиях ИК.')

insert Surgerys(Name, SurgeryDescription) values
('Surgery2',
' Пластика ДМПП,ДМЖП,ИСЛА,аномалия Эбштейна,радик.коррекц. тетрады Фалло.')

insert Surgerys(Name, SurgeryDescription) values
('Surgery3',
'Реконструкции пути оттока от ПЖ.')

/*Связываем пациентов с их операциями*/
insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(1, 1, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(2, 2, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(3, 3, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(4, 1, 0, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(5, 2, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(6, 3, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(7, 1, 0, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(8, 2, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(9, 3, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(10, 1, 0, '2001/12/06')

/*Добавляем для пациентов их докторов*/
insert PatientsToDoctors(PatientId, DoctorId) values(1, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(2, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(3, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(4, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(5, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(6, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(7, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(8, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(9, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(10, 12)

/*Добавить кандидатов*/
insert Candidates(Birthday, Card, LastName, FirstName, ThirdName, Login, Password, Mother, Phone1,
Policy, RoleId, Phone2, Token, StartDateOfObservation) values
('1992/07/25', '---', 'Сидоров', 'Андрей', 'Тихвинович',
 'log', 'pass', '', 212, '---', 1, 1232, 'token_value', '1998/07/12')

insert Candidates(Birthday, Card, LastName, FirstName, ThirdName, Login, Password, Mother, Phone1,
Policy, RoleId, Phone2, Token, StartDateOfObservation) values
('1992/07/25', '---', 'Сосницкий', 'Иван', 'Павлович',
 'log', 'pass', '', 212, '---', 1, 1232, 'token_value', '1998/09/12')

insert into DiagnosisClass(Name, Code) values('Болезни системы кровообращения', 'IX')
insert into DiagnosisClass(Name, Code) values('Врожденные аномалии [пороки крови], деформации и хромосомные нарушения', 'XVII')

insert into Diagnosis(Name, Code, DiagnosisClassId) values('Другие функциональные нарушения после операций на сердце', 'I97.1', 1)
insert into Diagnosis(Name, Code, DiagnosisClassId) values('Другие нарушения системы кровообращения после медицинских процедур, не классифицированные в других рубриках', 'I97.8', 1)
insert into Diagnosis(Name, Code, DiagnosisClassId) values('Дефект предсердной перегородки', 'Q21.1', 2)
insert into Diagnosis(Name, Code, DiagnosisClassId) values('Врожденный порок сердца неуточненный', 'Q24.9', 2)

insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 1)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 2)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 3)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 4)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (2, 4)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 5)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 6)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (2, 7)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 7)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (2, 8)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (2, 9)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (2, 10)
