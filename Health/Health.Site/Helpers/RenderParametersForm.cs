using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Health.Site.Models.Forms;

namespace Health.Site.Helpers
{
    /// <summary>
    /// Хелперы для работы с "параметрическими" формами.
    /// </summary>
    public static class RenderParametersForm
    {
        /// <summary>
        /// Создание разметки параметров.
        /// </summary>
        /// <param name="helper">Расширяемый объект.</param>
        /// <param name="form">Параметрическая форма.</param>
        /// <param name="prefix">Префикс для формирования ифентификаторов полей.</param>
        /// <returns>Разметка в html формате.</returns>
        public static HtmlString RenderParameters(this HtmlHelper helper, ParametersFormBase form, string prefix)
        {
            var params_string = new StringBuilder();
            string format = prefix + ".Parameters[{0}].{1}";
            for (int i = 0; i < form.Parameters.Count(); i++)
            {
                // <input> - имя параметра.
                var input_tag_name = new TagBuilder("input");
                input_tag_name.MergeAttribute("type", "hidden");
                input_tag_name.MergeAttribute("id", String.Format(format, i, "Name"));
                input_tag_name.MergeAttribute("name", String.Format(format, i, "Name"));
                input_tag_name.MergeAttribute("value", form.Parameters.ElementAt(i).Name);
                // </input>

                // <label> - метка для параметра.
                var label_tag_name = new TagBuilder("label");
                label_tag_name.MergeAttribute("for", String.Format(format, i, "Value"));
                label_tag_name.SetInnerText(form.Parameters.ElementAt(i).Name);
                // </label>

                // <input> - значение параметра.
                var input_tag_value = new TagBuilder("input");
                input_tag_value.MergeAttribute("id", String.Format(format, i, "Value"));
                input_tag_value.MergeAttribute("name", String.Format(format, i, "Value"));
                input_tag_value.MergeAttribute("value", form.Parameters.ElementAt(i).Value.ToString());
                // </input>

                // <p> - оборачиваем все в абзац.
                var p_tag = new TagBuilder("p")
                                {
                                    InnerHtml = input_tag_name.ToString(TagRenderMode.SelfClosing) +
                                                label_tag_name.ToString(TagRenderMode.Normal) +
                                                input_tag_value.ToString(TagRenderMode.SelfClosing) +
                                                helper.ValidationMessage(String.Format(format, i, "Value"))
                                };
                // </p>

                params_string.Append(p_tag);
            }
            
            return new HtmlString(params_string.ToString());
        }
    }
}