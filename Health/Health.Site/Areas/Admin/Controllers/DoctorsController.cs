using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Admin.Models;
using Health.Site.Controllers;

namespace Health.Site.Areas.Admin.Controllers
{
    public class DoctorsController : CoreController
    {
        public DoctorsController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        #region Show

        public ActionResult Show(int id)
        {
            Doctor doctor = CoreKernel.DoctorRepo.GetById(id);
            var form = new DoctorForm {Doctor = doctor};
            return View(form);
        }

        public ActionResult List()
        {
            var form = new DoctorList {Doctors = CoreKernel.DoctorRepo.GetAll()};
            return View(form);
        }

        #endregion
    }
}
