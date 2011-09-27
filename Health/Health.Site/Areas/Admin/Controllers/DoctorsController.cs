using System.Web.Mvc;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Admin.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Admin.Controllers
{
    public class DoctorsController : CoreController
    {
        public DoctorsController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        #region Show

        public ActionResult Show(int? id)
        {
            if (!id.HasValue) return RedirectTo<DoctorsController>(a => a.List());
            Doctor doctor = CoreKernel.DoctorRepo.GetById(id.Value);
            var form = new DoctorForm {Doctor = doctor};
            return 
                doctor == null
                    ? RedirectTo<DoctorsController>(a => a.List())
                    : View(form);
        }

        public ActionResult List()
        {
            var form = new DoctorList {Doctors = CoreKernel.DoctorRepo.GetAll()};
            return View(form);
        }

        #endregion

        #region Add

        [PRGImport]
        public ActionResult Add(DoctorForm form)
        {
            form.Specialties = DIKernel.Get<ISpecialtyRepository>().GetAll();
            return View(form);
        }

        [HttpPost, PRGExport]
        public ActionResult AddSubmit(DoctorForm form)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.DoctorRepo.Save(form.Doctor);
                form.Message = "Доктор добавлен";
                return RedirectTo<DoctorsController>(a => a.Confirm(form));
            }
            return RedirectTo<DoctorsController>(a => a.Add(form));
        }
        
        #endregion

        #region Edit

        [PRGImport]
        public ActionResult Edit(int? id, DoctorForm form)
        {
            if (!id.HasValue) return RedirectTo<DoctorsController>(a => a.List());
            form.Doctor = form.Doctor ?? CoreKernel.DoctorRepo.GetById(id.Value);
            form.Specialties = DIKernel.Get<ISpecialtyRepository>().GetAll();
            return
                form.Doctor == null
                    ? RedirectTo<DoctorsController>(a => a.List())
                    : View(form);
        }

        [HttpPost, PRGExport]
        public ActionResult Edit(DoctorForm form)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.DoctorRepo.Update(form.Doctor);
                form.Message = "Информация о докторе изменена";
                return RedirectTo<DoctorsController>(a => a.Confirm(form));
            }
            return RedirectTo<DoctorsController>(a => a.Edit(form.Doctor.Id, form));
        }
        
        #endregion

        #region Delete

        public ActionResult Delete(int? id, bool? confirm)
        {
            if (!id.HasValue) return RedirectTo<DoctorsController>(a => a.List());
            if (!confirm.HasValue)
            {
                Doctor doctor = CoreKernel.DoctorRepo.GetById(id.Value);
                var form = new DoctorForm {Doctor = doctor};
                return
                    doctor == null
                        ? RedirectTo<DoctorsController>(a => a.List())
                        : View(form);
            }
            if (confirm.Value) CoreKernel.DoctorRepo.DeleteById(id.Value);
            return RedirectTo<DoctorsController>(a => a.List());
        }

        #endregion

        #region Led Patients

        //public ActionResult AddLed(int doctor_id, int patient_id)
        //{
        //    Doctor doctor = CoreKernel.DoctorRepo.GetById(doctor_id);
        //    if (doctor == null) return RedirectTo<DoctorsController>(a => a.List());
        //    Patient patient = CoreKernel.PatientRepo.GetById(patient_id);
        //    if (patient == null) return RedirectTo<DoctorsController>(a => a.List());

        //}

        //public ActionResult RemoveLed(int doctor_id, int patient_id) {}

        #endregion

        #region Others

        [PRGImport]
        public ActionResult Confirm(DoctorForm form)
        {
            return View(form);
        }

        #endregion
    }
}