using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using Health.API.Entities;
using Health.API.Services;
using Ninject;

namespace Health.Site.Controllers
{
    public class HomeController : CoreController
    {
        public HomeController(IKernel di_kernel) : base(di_kernel)
        {
        }

        public ActionResult Index()
        {
            IEnumerable<IRole> roles = CoreKernel.RoleRepo.GetAll();
            
            var new_role = Entity<IRole>();
            new_role.Name = "new_role";
            new_role.Code = 4434;
            ViewBag.NewRole = new_role;

            ViewBag.VarName = new_role.PropertyName(x => x.Code);
            //ViewBag.VarName1 = new { new_role.Name }.GetName();
            Finder[] roless = GetBy(new[]
                                        {
                                            new Finder()
                                                {
                                                    Name = new_role.PropertyName(x => x.Name),
                                                    F = "some string"
                                                }
                                        });

            ViewData["Roles"] = roles;

            ViewBag.CountCandidates = CoreKernel.CandRepo.GetAll().Count();

            ViewBag.Message = "Добро пожаловать в ASP.NET MVC!";
            return View();
        }

        public class Finder
        {
            public string Name { get; set; }
            public string F { get; set; }
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

        public ActionResult About()
        {
            return View();
        }
    }

    public static class ObjectExtensions
    {
        public static string PropertyName<T, TOut>(this T source, Expression<Func<T, TOut>> property)
        {
            var member_expression = (MemberExpression)property.Body;
            return member_expression.Member.Name;
        }
    }
}