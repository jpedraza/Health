using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public CandidatesController(IDIKernel di_kernel) : base(di_kernel)
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
        
        public ActionResult AcceptBid(int id)
        {
            Candidate candidate = Get<ICandidateRepository>().GetById(id);
            Get<IRegistrationService>().AcceptBid(candidate);
            const string message = "Заявка для кандидата принята.";
            return RedirectTo<CandidatesController>(a => a.ConfirmBid(candidate, message), true);
        }

        public ActionResult RejectBid(int id)
        {
            Candidate candidate = Get<ICandidateRepository>().GetById(id);
            Get<IRegistrationService>().RejectBid(candidate);
            const string message = "Заявка для кандидата отклонена.";
            return RedirectTo<CandidatesController>(a => a.ConfirmBid(candidate, message), true);
        }

        [PRGImport(ParametersHook = true)]
        public ActionResult ConfirmBid(Candidate candidate, string message)
        {
            var form = new CandidateForm {Candidate = candidate, Message = message};
            return View(form);
        }

        #endregion
    }
}