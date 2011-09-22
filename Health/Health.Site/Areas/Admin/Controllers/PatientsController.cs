using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Admin.Models.Patients;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Admin.Controllers
{
    public class PatientsController : CoreController
    {
        public PatientsController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        [PRGImport(ParametersHook = true)]
        public ActionResult Add(PatientsForm form)
        {
            form.Patient = form.Patient ?? new Patient();
            return View(form);
        }

        [HttpPost, PRGExport(ParametersHook = true)]
        public ActionResult AddSubmit(PatientsForm form)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.PatientRepo.Save(form.Patient);
                return RedirectTo<PatientsController>(a => a.Confirm(form));
            }
            return RedirectTo<PatientsController>(a => a.Add(form));
        }

        [PRGImport(ParametersHook = true)]
        public ActionResult Confirm(PatientsForm form)
        {
            return View(form);
        }
    }
}
