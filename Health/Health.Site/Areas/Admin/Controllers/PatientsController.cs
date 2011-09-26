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

        public ActionResult Show(int id)
        {
            Patient patient = CoreKernel.PatientRepo.GetById(id);
            var form = new PatientForm {Patient = patient};
            return View(form);
        }

        public ActionResult List()
        {
            var form = new PatientList {Patients = CoreKernel.PatientRepo.GetAll()};
            return View(form);
        }

        #endregion

        #region Add

        [PRGImport(ParametersHook = true)]
        public ActionResult Add(PatientForm form)
        {
            form.Patient = form.Patient ?? new Patient();
            return View(form);
        }

        [HttpPost, PRGExport(ParametersHook = true)]
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

        #region Confirm

        [PRGImport(ParametersHook = true)]
        public ActionResult Confirm(PatientForm form)
        {
            return View(form);
        }

        #endregion
    }
}