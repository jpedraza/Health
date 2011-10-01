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
    public class CandidatesController : CoreController
    {
        public CandidatesController(IDIKernel diKernel) : base(diKernel)
        {
            
        }

        #region Show 

        public ActionResult Show(int id)
        {
            Candidate candidate = Get<ICandidateRepository>().GetById(id);
            var form = new CandidateForm {Candidate = candidate};
            return View(form);
        }

        public ActionResult List()
        {
            var form = new CandidateList {Candidates = Get<ICandidateRepository>().GetAll()};
            return View(form);
        }

        #endregion

        #region Bid
        
        [PRGImport, ValidationModel]
        public ActionResult AcceptBid([PRGInRoute]int? id, CandidateAcceptForm form)
        {
            if (!id.HasValue) return RedirectTo<CandidatesController>(a => a.List());
            form.Candidate = form.Candidate ?? Get<ICandidateRepository>().GetById(id.Value);
            if (form.Candidate == null) return RedirectTo<CandidatesController>(a => a.List());
            form.Doctors = Get<IDoctorRepository>().GetAll();
            return View(form);
        }

        [HttpPost, PRGExport, ValidationModel]
        public ActionResult AcceptBid(CandidateAcceptForm form)
        {
            if (ModelState.IsValid)
            {
                form.Candidate = Get<ICandidateRepository>().GetById(form.Candidate.Id);
                Get<IRegistrationService>().AcceptBid(form.Candidate, form.Doctor);
                const string message = "Заявка для кандидата принята.";
                return RedirectTo<CandidatesController>(a => a.ConfirmBid(form.Candidate, message));
            }
            return RedirectTo<CandidatesController>(a => a.AcceptBid(form.Candidate.Id, form));
        }

        public ActionResult RejectBid(int id)
        {
            Candidate candidate = Get<ICandidateRepository>().GetById(id);
            Get<IRegistrationService>().RejectBid(candidate);
            const string message = "Заявка для кандидата отклонена.";
            return RedirectTo<CandidatesController>(a => a.ConfirmBid(candidate, message));
        }

        [PRGImport]
        public ActionResult ConfirmBid(Candidate candidate, string message)
        {
            var form = new CandidateForm {Candidate = candidate, Message = message};
            return View(form);
        }

        #endregion
    }
}