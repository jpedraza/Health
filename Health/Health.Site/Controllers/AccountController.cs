using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Health.API.Entities;
using Health.API.Services;
using Health.Data.Entities;
using Health.Site.Models;
using Health.Site.Models.Forms;
using Ninject;

namespace Health.Site.Controllers
{
    public class AccountController : CoreController
    {
        public AccountController(IKernel di_kernel) : base(di_kernel)
        {
        }

        /// <summary>
        /// Отображение формы входа
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login_form_model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "LoginForm")]AccountViewModel login_form_model)
        {
            if (ModelState.IsValid)
            {
                if (CoreKernel.AuthServ.Login(login_form_model.LoginForm.Login, login_form_model.LoginForm.Password,
                                            login_form_model.LoginForm.RememberMe))
                {
                    return RedirectToRoute("Admin");
                }
            }
            return View(login_form_model);
        }

        /// <summary>
        /// Выход пользователя
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            CoreKernel.AuthServ.Logout();

            return RedirectToRoute("Home");
        }

        /// <summary>
        /// Отображение формы регистрации
        /// </summary>
        /// <returns></returns>
        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Обработка запроса на регистрацию
        /// </summary>
        /// <param name="form_model">Модель формы регистрации</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "RegistrationForm")]AccountViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.RegServ.SaveBid(form_model.RegistrationForm);
                return RedirectToRoute("Home");
            }
            return View(form_model);
        }

        /// <summary>
        /// Отображение формы опроса
        /// </summary>
        /// <returns></returns>
        public ActionResult Interview()
        {
            var form_model = new InterviewFormModel()
                                 {
                                     Parameters = new List<Parameter>()
                                                      {
                                                          new Parameter()
                                                              {
                                                                  Name = "P1",
                                                                  Value = "V1"
                                                              },
                                                          new Parameter()
                                                              {
                                                                  Name = "P2",
                                                                  Value = "V2"
                                                              }
                                                      }
                                 };
            var acc_view_model = new AccountViewModel()
                                     {
                                         InterviewForm = form_model
                                     };
            return View(acc_view_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Interview([Bind(Include = "InterviewForm")]AccountViewModel form_model)
        {
            return View(form_model);
        }
    }
}