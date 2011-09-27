using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Admin.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Admin.Controllers
{
    public class PatientsController : CoreController
    {
        public PatientsController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        #region Show

        public ActionResult Show(int? id)
        {
            if (!id.HasValue) return RedirectTo<PatientsController>(a => a.List());
            Patient patient = CoreKernel.PatientRepo.GetById(id.Value);
            var form = new PatientForm {Patient = patient};
            return 
                patient == null
                    ? RedirectTo<PatientsController>(a => a.List())
                    : View(form);
        }

        public ActionResult List()
        {
            var form = new PatientList {Patients = CoreKernel.PatientRepo.GetAll()};
            return View(form);
        }

        #endregion

        #region Add

        [PRGImport]
        public ActionResult Add(PatientForm form)
        {
            form.Patient = form.Patient ?? new Patient();
            return View(form);
        }

        [HttpPost, PRGExport]
        public ActionResult AddSubmit(PatientForm form)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.PatientRepo.Save(form.Patient);
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
            form.Patient = form.Patient ?? CoreKernel.PatientRepo.GetById(id.Value);
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
                CoreKernel.PatientRepo.Update(form.Patient);
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
                Patient patient = CoreKernel.PatientRepo.GetById(id.Value);
                var form = new PatientForm {Patient = patient};
                return
                    patient == null
                        ? RedirectTo<PatientsController>(a => a.List())
                        : View(form);
            }
            if (confirm.Value) CoreKernel.PatientRepo.DeleteById(id.Value);
            return RedirectTo<PatientsController>(a => a.List());
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