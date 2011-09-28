using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.API.Services;
using Health.Core.API;
using Health.Core.Entities.POCO;

namespace Health.Core.Services
{
    public class AttendingDoctorService : CoreService, IAttendingDoctorService
    {
        public AttendingDoctorService(IDIKernel di_kernel, ICoreKernel core_kernel)
            : base(di_kernel, core_kernel)
        {
        }

        /// <summary>
        /// Изменить лечащего доктора у пациента.
        /// </summary>
        /// <param name="doctor_id">Идентификатор доктора.</param>
        /// <param name="patient_id">Идентификатор пациента.</param>
        public void SetLedDoctorForPatient(int doctor_id, int patient_id)
        {
            Doctor doctor = CoreKernel.DoctorRepo.GetByIdIfNotLedPatient(doctor_id, patient_id);
            Patient patient = CoreKernel.PatientRepo.GetByIdIfNotLedDoctor(patient_id, doctor_id);
            if (doctor != null && patient != null)
            {
                doctor.Patients.ToList().Add(patient);
                patient.Doctor.Patients.ToList().Remove(patient);
                patient.Doctor = doctor;
                CoreKernel.DoctorRepo.Update(doctor);
                CoreKernel.DoctorRepo.Update(patient.Doctor);
                CoreKernel.PatientRepo.Update(patient);
            }            
        }
    }
}
