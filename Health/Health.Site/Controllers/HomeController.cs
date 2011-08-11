using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Health.API;
using Health.API.Entities;

namespace Health.Site.Controllers
{
    public class HomeController : CoreController
    {
        public HomeController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        public ActionResult Index()
        {
            IEnumerable<IRole> roles = CoreKernel.RoleRepo.GetAll();

            var new_role = Instance<IRole>(o =>
                                               {
                                                   o.Name = "new_role";
                                                   o.Code = 4323;
                                               });
            ViewBag.NewRole = new_role;

            ViewBag.VarName = new_role.PropertyName(x => x.Code);
            //ViewBag.VarName1 = new { new_role.Name }.GetName();
            Finder[] roless = GetBy(new[]
                                        {
                                            new Finder
                                                {
                                                    Name = new_role.PropertyName(x => x.Name),
                                                    F = "some string"
                                                }
                                        });

            ViewData["Roles"] = roles;

            ViewBag.CountCandidates = CoreKernel.CandRepo.GetAll().Count();

            ViewBag.PName = new_role.PName(n => n.Name);

            ViewBag.Message = "Добро пожаловать в ASP.NET MVC!";
            return View();
        }

        public Finder[] GetBy(params Finder[] f)
        {
            return f;
        }

        /*public string GetName(Func<IRole, object> func)
        {
            //return func.ToString();

            return new { func.Target }.GetName();
        }

        static string GetName<T>(Expression<Func<T>> expr)
        {
            return ((MemberExpression)expr.Body).Member.Name;
        }

        static string GetName<T>(T item) where T : class
        {
            var tp = typeof(T);
            PropertyInfo[] property_infos = tp.GetProperties();
            return property_infos[0].Name;
        }*/

        #region Nested type: Finder

        public class Finder
        {
            public string Name { get; set; }
            public string F { get; set; }
        }

        #endregion
    }

    public static class ObjectExtensions
    {
        public static string PropertyName<T, TOut>(this T source, Expression<Func<T, TOut>> property)
        {
            var member_expression = (MemberExpression) property.Body;
            return member_expression.Member.Name;
        }

        public static string PName<T, TOut>(this T source, Expression<Func<T, TOut>> action)
        {
            var binary_expression = action.Body as BinaryExpression;
            return "";
        }
    }
}