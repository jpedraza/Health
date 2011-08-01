using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.API;
using Health.API.Entities;
using Health.Data.Entities;
using Health.Site.Models.Forms;

namespace Health.Site.Models.Binders
{
    public class InterviewFormBinder : DefaultModelBinder
    {
        protected IDIKernel Kernel { get; set; }

        public InterviewFormBinder(IDIKernel kernel)
        {
            Kernel = kernel;
        }

        protected override object CreateModel(ControllerContext controller_context, ModelBindingContext binding_context, Type model_type)
        {
            var interview_form = new InterviewFormModel { Parameters = GetValueForParameter(controller_context, binding_context) };
            return interview_form;
        }

        protected override void BindProperty(ControllerContext controller_context, ModelBindingContext binding_context, System.ComponentModel.PropertyDescriptor property_descriptor)
        {
            SetProperty(controller_context, binding_context, property_descriptor, GetValueForParameter(controller_context, binding_context));
        }

        protected IEnumerable<IParameter> GetValueForParameter(ControllerContext controller_context, ModelBindingContext binding_context)
        {
            NameValueCollection value_collection = controller_context.HttpContext.Request.Form;

            var parameters = Kernel.Get<IEnumerable<IParameter>>();
            List<IParameter> list_parameters = parameters.ToList();

            int count = 0;
            foreach (var key in value_collection)
            {
                if (key.ToString().Contains("Parameters"))
                {
                    count++;
                }
            }
            const string format = "InterviewForm.Parameters[{0}].{1}";
            for (int i = 0; i < count / 2; i++)
            {
                var parameter = Kernel.Get<IParameter>();
                parameter.Name = value_collection[String.Format(format, i, "Name")];
                parameter.Value = value_collection[String.Format(format, i, "Value")];
                list_parameters.Add(parameter);
            }

            return list_parameters;
        }
    }
}