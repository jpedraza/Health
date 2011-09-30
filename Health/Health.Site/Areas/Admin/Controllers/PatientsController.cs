using System.Web.Mvc;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.API.Services;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Admin.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Admin.Controllers
{
    public class PatientsController : CoreController
    {
        public PatientsController(IDIKernel diKernel) : base(diKernel)
        {
        }

        #region Show

        public ActionResult Show(int? id)
        {
            if (!id.HasValue) return RedirectTo<PatientsController>(a => a.List());
            Patient patient = Get<IPatientRepository>().GetById(id.Value);
            var form = new PatientForm {Patient = patient};
            return 
                patient == null
                    ? RedirectTo<PatientsController>(a => a.List())
                    : View(form);
        }

        public ActionResult List()
        {
            var form = new PatientList {Patients = Get<IPatientRepository>().GetAll()};
            return View(form);
        }

        #endregion

        #region Add

        [PRGImport]
        public ActionResult Add(PatientForm form)
        {
            form.Patient = form.Patient ?? new Patient();
            form.Doctors = Get<IDoctorRepository>().GetAll();
            return View(form);
        }

        [HttpPost, PRGExport]
        public ActionResult AddSubmit(PatientForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IPatientRepository>().Save(form.Patient);
                return RedirectTo<PatientsController>(a => a.Confirm(form));
            }
            return RedirectTo<PatientsController>(a => a.Add(form));
        }

        #endregion

        #region Edit
        
        [PRGImport]
        public ActionResult Edit(int? id, PatientForm form)
        {
            if (!id.HasValue) return RedirectTo<PatientsController>(a => a.List());
            form.Patient = form.Patient ?? Get<IPatientRepository>().GetById(id.Value);
            form.Doctors = Get<IDoctorRepository>().GetAll();
            return
                form.Patient == null
                    ? RedirectTo<PatientsController>(a => a.List())
                    : View(form);
        }

        [HttpPost, PRGExport]
        public ActionResult Edit(PatientForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IPatientRepository>().Update(form.Patient);
                Get<IAttendingDoctorService>().SetLedDoctorForPatient(form.Patient.Doctor.Id, form.Patient.Id);
                form.Message = "Пациент отредактирован";
                return RedirectTo<PatientsController>(a => a.Confirm(form));
            }
            return RedirectTo<PatientsController>(a => a.Edit(form.Patient.Id, form));
        }

        #endregion

        #region Delete

        public ActionResult Delete(int? id, bool? confirm)
        {
            if (!id.HasValue) return RedirectTo<PatientsController>(a => a.List());
            if (!confirm.HasValue)
            {
                Patient patient = Get<IPatientRepository>().GetById(id.Value);
                var form = new PatientForm {Patient = patient};
                return
                    patient == null
                        ? RedirectTo<PatientsController>(a => a.List())
                        : View(form);
            }
            if (confirm.Value) Get<IPatientRepository>().DeleteById(id.Value);
            return RedirectTo<PatientsController>(a => a.List());
        }

        #endregion

        #region Led Doctor

        public ActionResult Led(int? id)
        {
            if (!id.HasValue) return RedirectTo<PatientsController>(a => a.List());
            Patient patient = Get<IPatientRepository>().GetById(id.Value);
            if (patient == null) return RedirectTo<PatientsController>(a => a.List());
            var form = new PatientForm {Patient = patient, Doctors = Get<IDoctorRepository>().GetAll()};
            return View(form);
        }

        #endregion

        #region Confirm

        [PRGImport]
        public ActionResult Confirm(PatientForm form)
        {
            return 
                form.Patient == null
                    ? RedirectTo<PatientsController>(a => a.List())
                    : View(form);
        }

        #endregion
    }
}