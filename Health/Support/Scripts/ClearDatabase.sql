delete from PatientsToDiagnosis
delete from PatientsToDoctors
delete from PatientsToSurgerys
delete from FunctionalDisordersToPatients
DBCC CHECKIDENT([Diagnosis], RESEED, 0)
delete from Appointments
DBCC CHECKIDENT(Appointments, RESEED, 0)
delete from Candidates
DBCC CHECKIDENT(Candidates, RESEED, 0)
delete from DefaultSchedule
DBCC CHECKIDENT(DefaultSchedule, RESEED, 0)
delete from Diagnosis
DBCC CHECKIDENT(Diagnosis, RESEED, 0)
delete from DiagnosisClass
DBCC CHECKIDENT(DiagnosisClass, RESEED, 0)
delete from Patients
delete from Doctors
delete from FunctionalClasses
DBCC CHECKIDENT(FunctionalClasses, RESEED, 0)
delete from FunctionalDisorders
DBCC CHECKIDENT(FunctionalDisorders, RESEED, 0)
delete from Parameters
DBCC CHECKIDENT(Parameters, RESEED, 0)
delete from PersonalSchedule
DBCC CHECKIDENT(PersonalSchedule, RESEED, 0)
delete from Users
DBCC CHECKIDENT(Users, RESEED, 0)
delete from Roles
DBCC CHECKIDENT(Roles, RESEED, 0)
delete from Specialties
DBCC CHECKIDENT(Specialties, RESEED, 0)
delete from Surgerys
DBCC CHECKIDENT(Surgerys, RESEED, 0)