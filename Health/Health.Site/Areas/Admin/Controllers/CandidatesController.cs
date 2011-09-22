using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Areas.Admin.Models;
using Health.Site.Controllers;

namespace Health.Site.Areas.Admin.Controllers
{
    public class CandidatesController : CoreController
    {
        public CandidatesController(IDIKernel di_kernel) : base(di_kernel)
        {
            
        }

        #region Show 

        public ActionResult List()
        {
            var form = new CandidateList {Candidates = CoreKernel.CandRepo.GetAll()};
            return View(form);
        }

        #endregion
    }
}
