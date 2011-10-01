using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Health.Core.Entities;
using Health.Core.Entities.POCO;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Health.Site.Factory
{
    /// <summary>
    /// Интерфейс для отрисовщиков.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Получить разметку.
        /// </summary>
        /// <returns>Разметка.</returns>
        HtmlString GetLayout();
    }

    public sealed class RenderingFactory
    {
        /// <summary>
        /// Отрисовываемый параметр
        /// </summary>
        private Parameter parameter { set; get; }

        /// <summary>
        /// Префикс для генерации имен Html-объектов
        /// </summary>
        private string name_prefix { get; set; }

        /// <summary>
        /// Строка1 для подключения JQ
        /// </summary>
        private const string jquery_connect_string1 = "/Health.Site/Scripts/jquery.validate.min.js";

        /// <summary>
        /// Строка2 для подключения JQ
        /// </summary>
        private const string jquery_connect_string2 = "/Health.Site/Scripts/jquery.validate.unobtrusive.min.js";

        /// <summary>
        /// Создает HTML-строку подключения JQ
        /// </summary>
        /// <returns>HTML-строка</returns>
        private HtmlString create_jquery_connect()
        {
            var script_tag1 = new TagBuilder("script");
            script_tag1.MergeAttribute("src", jquery_connect_string1);
            script_tag1.MergeAttribute("type", "text/javascript");

            var script_tag2 = new TagBuilder("script");
            script_tag2.MergeAttribute("src", jquery_connect_string2);
            script_tag2.MergeAttribute("type", "text/javascript");

            return new HtmlString(script_tag1.ToString(TagRenderMode.Normal)+script_tag2.ToString(TagRenderMode.Normal));
        }
        /// <summary>
        /// Инициализирует фабрику для отрисовки параметра.
        /// </summary>
        /// <param name="parameter">Отрисовываемый параметр</param>
        /// <param name="name_prefix">Префикс для генерации имен Html-объектов</param>
        public RenderingFactory(Parameter parameter, string name_prefix)
        {
            this.parameter = parameter;
            this.name_prefix = name_prefix;
        }

        public HtmlString GetLayout()
        {
            HtmlString layout = new HtmlString("");
            //Перед выбором отрисовщика, необходиом подключить библиотеку JQuery
            layout = create_jquery_connect();
            if (!parameter.MetaData.Is_var)
            {
                var _object = new Dictionary<string, object>();
                _object["Name"] = parameter.Name;
                _object["Value"] = parameter.Value;

                var renderer = new TextBoxRenderer(_object, name_prefix);
                return new HtmlString(layout.ToString() + renderer.GetLayout());
            }
            else
            {
                var _object = new Dictionary<string, object>();
                _object["Name"] = parameter.Name;
                var _Variants = new Dictionary<string, string>();
                for (int i = 0; i < parameter.MetaData.Variants.Length; i++)
                {
                    _Variants.Add(String.Format("name-{0}", i), parameter.MetaData.Variants[i].Value);
                }
                _object["Variants"] = _Variants;
                if (parameter.MetaData.Variants.Length <= BaseRenderer.limit_length_of_list)
                {
                    var renderer = new ListBoxRenderer(_object, name_prefix);
                    return new HtmlString(layout.ToString() + renderer.GetLayout());
                }
                else
                {
                    var renderer = new DropDownBoxRenderer(_object, name_prefix);
                    return new HtmlString(layout.ToString() + renderer.GetLayout());
                }
            }
        }
    }   
 
    public abstract class BaseRenderer
    {
        /// <summary>
        /// Объект предназначенный для отрисовки
        /// </summary>
        protected object object_for_rendering {get; set;}

        /// <summary>
        /// Префикс для генерации имен Html-объектов
        /// </summary>
        protected string name_prefix { get; set; }
        /// <summary>
        /// Иницилизирует объекта класса отрисовщика
        /// </summary>
        /// <param name="object_for_rendering">Объект предметной области, предназначенный для отрисовки</param>
        /// <param name="name_prefix">Префикс для генерации имен Html-объектов</param>
        public BaseRenderer(object object_for_rendering, string name_prefix)
        {
            this.object_for_rendering = object_for_rendering;
            this.name_prefix = name_prefix;
        }

        /// <summary>
        /// Предельная граница длины списка вариантов. Если число вариантов больше, чем эта 
        /// граница, то отрисуется DropDownBox. Если меньше, то listbox.
        /// </summary>
        public const int limit_length_of_list = 4;
    }

    public sealed class TextBoxRenderer : BaseRenderer, IRenderer
    {
        public TextBoxRenderer(object object_for_rendering, string name_prefix) : base(object_for_rendering, name_prefix) { }

        public HtmlString GetLayout()
        {
            var layout = new StringBuilder();
            var _object = object_for_rendering as Dictionary<string, object>;
            if (_object == null)
            {
                throw new Exception("Невозможно отрисовать параметр для пользователя.");
            }
            var label_tag = new TagBuilder("label");
            label_tag.MergeAttribute("for", String.Format("{0}_Value", name_prefix));
            label_tag.SetInnerText(_object["Name"].ToString());

            var span_tag = new TagBuilder("span");
            span_tag.MergeAttribute("class", "field-validation-valid");
            span_tag.MergeAttribute("data-valmsg-for", String.Format("{0}.Value", name_prefix));
            span_tag.MergeAttribute("data-valmsg-replace", "true");
            span_tag.SetInnerText("");

            var input_tag = new TagBuilder("input");
            input_tag.MergeAttribute("data-val", "true");
            input_tag.MergeAttribute("data-val-required", "Введите значение!");
            input_tag.MergeAttribute("id", String.Format("{0}_Value", name_prefix));
            input_tag.MergeAttribute("name", String.Format("{0}.Value", name_prefix));
            input_tag.MergeAttribute("type", "text");

            var div_tag = new TagBuilder("div");
            div_tag.InnerHtml = new TagBuilder("p") { InnerHtml = label_tag.ToString(TagRenderMode.Normal) }.ToString(TagRenderMode.Normal);
            div_tag.InnerHtml += new TagBuilder("p") { InnerHtml = span_tag.ToString(TagRenderMode.Normal) }.ToString(TagRenderMode.Normal);
            div_tag.InnerHtml += new TagBuilder("p") { InnerHtml = input_tag.ToString(TagRenderMode.SelfClosing) }.ToString(TagRenderMode.Normal);
            
            return new HtmlString(div_tag.ToString(TagRenderMode.Normal));
        }        
    }

    public sealed class TextAreaRenderer : BaseRenderer, IRenderer
    {
        public TextAreaRenderer(object object_for_rendering, string name_prefix) : base(object_for_rendering, name_prefix) { }
        public HtmlString GetLayout()
        {
            var layout = new StringBuilder();
            var _object = object_for_rendering as Dictionary<string, object>;
            if (_object == null)
            {
                throw new Exception("Невозможно отрисовать параметр для пользователя.");
            }
            var label_tag = new TagBuilder("label");
            label_tag.MergeAttribute("for", String.Format("{0}_Value", name_prefix));
            label_tag.SetInnerText(_object["Name"].ToString());

            var span_tag = new TagBuilder("span");
            span_tag.MergeAttribute("class", "field-validation-valid");
            span_tag.MergeAttribute("data-valmsg-for", String.Format("{0}.Value", name_prefix));
            span_tag.MergeAttribute("data-valmsg-replace", "true");
            span_tag.SetInnerText("");

            var input_tag = new TagBuilder("textarea");
            input_tag.MergeAttribute("data-val", "true");
            input_tag.MergeAttribute("data-val-required", "Введите значение!");
            input_tag.MergeAttribute("id", String.Format("{0}_Value", name_prefix));
            input_tag.MergeAttribute("name", String.Format("{0}.Value", name_prefix));
            input_tag.MergeAttribute("type", "text");

            var div_tag = new TagBuilder("div");
            div_tag.InnerHtml = new TagBuilder("p") { InnerHtml = label_tag.ToString(TagRenderMode.Normal) }.ToString(TagRenderMode.Normal);
            div_tag.InnerHtml += new TagBuilder("p") { InnerHtml = span_tag.ToString(TagRenderMode.Normal) }.ToString(TagRenderMode.Normal);
            div_tag.InnerHtml += new TagBuilder("p") { InnerHtml = input_tag.ToString(TagRenderMode.SelfClosing) }.ToString(TagRenderMode.Normal);

            return new HtmlString(div_tag.ToString(TagRenderMode.Normal));
        }
    }

    public sealed class ListBoxRenderer : BaseRenderer, IRenderer
    {
        public ListBoxRenderer(object object_for_rendering, string name_prefix) : base(object_for_rendering, name_prefix) { }

        public HtmlString GetLayout()
        {
            var div_tag = new TagBuilder("div");

            var _object = object_for_rendering as Dictionary<string, object>;
            var _name = _object["Name"] as string;
            var _variants = _object["Variants"] as Dictionary<string, string>;
            if (_object == null || _name == null || _variants == null)
            {
                throw new Exception("Невозможно отрисовать параметр для пользователя.");
            }
            var select_tag = new TagBuilder("select");
            select_tag.MergeAttribute("multiple", "multiple");
            select_tag.MergeAttribute("size", limit_length_of_list.ToString());
            foreach (var variant in _variants)
            {
                var _option_tag = new TagBuilder("option");
                _option_tag.MergeAttribute("id", variant.Key);
                _option_tag.MergeAttribute("name", variant.Key);
                _option_tag.SetInnerText(variant.Value);
                select_tag.InnerHtml += _option_tag.ToString(TagRenderMode.Normal);
            }

            div_tag.InnerHtml = select_tag.ToString(TagRenderMode.Normal);
            return new HtmlString(div_tag.ToString(TagRenderMode.Normal));
        }
    }

    public sealed class DropDownBoxRenderer : BaseRenderer, IRenderer
    {
        public DropDownBoxRenderer(object object_for_rendering, string name_prefix) : base(object_for_rendering, name_prefix) { }

        public HtmlString GetLayout()
        {
            var div_tag = new TagBuilder("div");

            var _object = object_for_rendering as Dictionary<string, object>;
            var _name = _object["Name"] as string;
            var _variants = _object["Variants"] as Dictionary<string, string>;
            if (_object == null || _name == null || _variants == null)
            {
                throw new Exception("Невозможно отрисовать параметр для пользователя.");
            }
            var select_tag = new TagBuilder("select");
            foreach (var variant in _variants)
            {
                var _option_tag = new TagBuilder("option");
                _option_tag.MergeAttribute("id", variant.Key);
                _option_tag.MergeAttribute("name", variant.Key);
                _option_tag.SetInnerText(variant.Value);
                select_tag.InnerHtml += _option_tag.ToString(TagRenderMode.Normal);
            }

            div_tag.InnerHtml = select_tag.ToString(TagRenderMode.Normal);
            return new HtmlString(div_tag.ToString(TagRenderMode.Normal));
        }
    }
}