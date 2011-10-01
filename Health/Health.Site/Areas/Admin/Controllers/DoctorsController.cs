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
        public DoctorsController(IDIKernel diKernel) : base(diKernel)
        {
        }

        #region Show

        public ActionResult Show(int? id)
        {
            if (!id.HasValue) return RedirectTo<DoctorsController>(a => a.List());
            Doctor doctor = Get<IDoctorRepository>().GetById(id.Value);
            if (doctor == null) return RedirectTo<DoctorsController>(a => a.List());
            var form = new DoctorForm {Doctor = doctor};
            return View(form);
        }

        public ActionResult List()
        {
            var form = new DoctorList {Doctors = Get<IDoctorRepository>().GetAll()};
            return View(form);
        }

        #endregion

        #region Add

        [PRGImport, ValidationModel]
        public ActionResult Add(DoctorForm form)
        {
            form.Specialties = DIKernel.Get<ISpecialtyRepository>().GetAll();
            return View(form);
        }

        [HttpPost, PRGExport, ValidationModel]
        public ActionResult AddSubmit(DoctorForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IDoctorRepository>().Save(form.Doctor);
                form.Message = "Доктор добавлен";
                return RedirectTo<DoctorsController>(a => a.Confirm(form));
            }
            return RedirectTo<DoctorsController>(a => a.Add(form));
        }
        
        #endregion

        #region Edit

        [PRGImport, ValidationModel]
        public ActionResult Edit(int? id, DoctorForm form)
        {
            if (!id.HasValue) return RedirectTo<DoctorsController>(a => a.List());
            form.Doctor = form.Doctor ?? Get<IDoctorRepository>().GetById(id.Value);
            if (form.Doctor == null) return RedirectTo<DoctorsController>(a => a.List());
            form.Specialties = DIKernel.Get<ISpecialtyRepository>().GetAll();
            return View(form);
        }

        [HttpPost, PRGExport, ValidationModel]
        public ActionResult Edit(DoctorForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IDoctorRepository>().Update(form.Doctor);
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
                Doctor doctor = DIKernel.Get<IDoctorRepository>().GetById(id.Value);
                if (doctor == null) return RedirectTo<DoctorsController>(a => a.List());
                var form = new DoctorForm {Doctor = doctor};
                return View(form);
            }
            if (confirm.Value) DIKernel.Get<IDoctorRepository>().DeleteById(id.Value);
            return RedirectTo<DoctorsController>(a => a.List());
        }

        #endregion

        #region Others

        [PRGImport]
        public ActionResult Confirm(DoctorForm form)
        {
            return 
                form.Doctor == null
                    ? RedirectTo<DoctorsController>(a => a.List())
                    : View(form);
        }

        #endregion
    }
}