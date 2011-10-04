using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Health.Core.Entities;
using Health.Core.Entities.POCO;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
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
        HtmlString GetLayout(object object_for_rendering, string name_prefix, string name_element);
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
        /// Инициализирует фабрику для отрисовки параметра.
        /// </summary>
        /// <param name="parameter">Отрисовываемый параметр</param>
        /// <param name="name_prefix">Префикс для генерации имен Html-объектов</param>
        public RenderingFactory(Parameter parameter, string name_prefix)
        {
            this.parameter = parameter;
            this.name_prefix = name_prefix;
        }

        public HtmlString GetPatientParameterLayout()
        {
            var name_element = "Value";
            HtmlString layout = new HtmlString("");
            if (!parameter.MetaData.Is_var)
            {
                var _object = new Dictionary<string, object>();
                _object["Name"] = parameter.Name;
                _object["Value"] = parameter.Value;

                var renderer = new TextBoxRenderer();
                return new HtmlString(renderer.GetLayout(_object, name_prefix, name_element).ToString());
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
                    var renderer = new ListBoxRenderer();
                    return new HtmlString(renderer.GetLayout(_object, name_prefix, name_element).ToString());
                }
                else
                {
                    var renderer = new DropDownBoxRenderer();
                    return new HtmlString(renderer.GetLayout(_object, name_prefix, name_element).ToString());
                }
            }
        }

        public HtmlString GetAddParameterLayout(Parameter parameter)
        {
            var type = parameter.GetType();
            var FieldInfos = type.GetProperties();

            var div_tag = new TagBuilder("div");
            div_tag.InnerHtml += "<h4>Заполните пожалуйста основные характеристики параметра</h4>";
            
            var textbox_render = new TextBoxRenderer();
            foreach (PropertyInfo field_info in FieldInfos)
            {
                var dict = new Dictionary<string, object>();
                dict["Name"] = field_info.Name;
                dict["Value"] = new object();                

                if (field_info.Name != "MetaData" && field_info.Name != "Id")
                {
                    div_tag.InnerHtml += textbox_render.GetLayout(dict, name_prefix, field_info.Name);
                }
                else
                {
                    if (field_info.Name == "MetaData" && field_info.PropertyType == typeof(MetaData))
                    {
                        div_tag.InnerHtml += "<h4>Заполните пожалуйста мета-данные</h4>";

                        var metadata_type = typeof(MetaData);
                        var fieldInfos2 = metadata_type.GetProperties();
                        var checkbox_render = new CheckBoxRenderer();
                        foreach (PropertyInfo fieldinfo2 in fieldInfos2)
                        {
                            if (fieldinfo2.PropertyType == typeof(bool))
                            {
                                var dict2 = new Dictionary<string, object>();
                                dict2["Name"] = fieldinfo2.Name;
                                dict2["Value"] = new bool();

                                div_tag.InnerHtml += checkbox_render.GetLayout(dict2, name_prefix, fieldinfo2.Name);
                            }

                            if (fieldinfo2.PropertyType == typeof(object) || fieldinfo2.PropertyType == typeof(Nullable<int>))
                            {
                                var dict2 = new Dictionary<string, object>();
                                dict2["Name"] = fieldinfo2.Name;
                                dict2["Value"] = new object();

                                div_tag.InnerHtml += textbox_render.GetLayout(dict2, name_prefix, fieldinfo2.Name);
                            }
                        }
                    }
                }
            }
            return new HtmlString(div_tag.ToString(TagRenderMode.Normal));
        }
    }   
 
    public abstract class BaseRenderer
    {
        /// <summary>
        /// Предельная граница длины списка вариантов. Если число вариантов больше, чем эта 
        /// граница, то отрисуется DropDownBox. Если меньше, то listbox.
        /// </summary>
        public const int limit_length_of_list = 4;
    }

    public sealed class TextBoxRenderer : BaseRenderer, IRenderer
    {

        public HtmlString GetLayout(object object_for_rendering, string name_prefix, string name_element)
        {
            var layout = new StringBuilder();
            var _object = object_for_rendering as Dictionary<string, object>;
            if (_object == null)
            {
                throw new Exception("Невозможно отрисовать параметр для пользователя.");
            }
            var label_tag = new TagBuilder("label");
            label_tag.MergeAttribute("for", String.Format("{0}_{1}", name_prefix, name_element));
            label_tag.SetInnerText(_object["Name"].ToString());

            var span_tag = new TagBuilder("span");
            span_tag.MergeAttribute("class", "field-validation-valid");
            span_tag.MergeAttribute("data-valmsg-for", String.Format("{0}.{1}", name_prefix, name_element));
            span_tag.MergeAttribute("data-valmsg-replace", "true");
            span_tag.SetInnerText("");

            var input_tag = new TagBuilder("input");
            input_tag.MergeAttribute("data-val", "true");
            input_tag.MergeAttribute("data-val-required", "Введите значение!");
            input_tag.MergeAttribute("id", String.Format("{0}_{1}", name_prefix, name_element));
            input_tag.MergeAttribute("name", String.Format("{0}.{1}", name_prefix, name_element));
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
        public HtmlString GetLayout(object object_for_rendering, string name_prefix, string name_element)
        {
            var layout = new StringBuilder();
            var _object = object_for_rendering as Dictionary<string, object>;
            if (_object == null)
            {
                throw new Exception("Невозможно отрисовать параметр для пользователя.");
            }
            var label_tag = new TagBuilder("label");
            label_tag.MergeAttribute("for", String.Format("{0}_{1}", name_prefix, name_element));
            label_tag.SetInnerText(_object["Name"].ToString());

            var span_tag = new TagBuilder("span");
            span_tag.MergeAttribute("class", "field-validation-valid");
            span_tag.MergeAttribute("data-valmsg-for", String.Format("{0}.{1}", name_prefix, name_element));
            span_tag.MergeAttribute("data-valmsg-replace", "true");
            span_tag.SetInnerText("");

            var textareatag = new TagBuilder("textarea");
            textareatag.MergeAttribute("data-val", "true");
            textareatag.MergeAttribute("data-val-required", "Введите значение!");
            textareatag.MergeAttribute("id", String.Format("{0}_{1}", name_prefix, name_element));
            textareatag.MergeAttribute("name", String.Format("{0}.{1}", name_prefix, name_element));
            textareatag.MergeAttribute("type", "text");

            var div_tag = new TagBuilder("div");
            div_tag.InnerHtml = new TagBuilder("p") { InnerHtml = label_tag.ToString(TagRenderMode.Normal) }.ToString(TagRenderMode.Normal);
            div_tag.InnerHtml += new TagBuilder("p") { InnerHtml = span_tag.ToString(TagRenderMode.Normal) }.ToString(TagRenderMode.Normal);
            div_tag.InnerHtml += new TagBuilder("p") { InnerHtml = textareatag.ToString(TagRenderMode.Normal) }.ToString(TagRenderMode.Normal);

            return new HtmlString(div_tag.ToString(TagRenderMode.Normal));
        }
    }

    public sealed class ListBoxRenderer : BaseRenderer, IRenderer
    {
        public HtmlString GetLayout(object object_for_rendering, string name_prefix, string name_element)
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
        public HtmlString GetLayout(object object_for_rendering, string name_prefix, string name_element)
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

    public sealed class CheckBoxRenderer : BaseRenderer, IRenderer
    {
        public HtmlString GetLayout(object object_for_rendering, string name_prefix, string name_element)
        {
            var layout = new StringBuilder();
            var _object = object_for_rendering as Dictionary<string, object>;
            if (_object == null)
            {
                throw new Exception("Невозможно отрисовать параметр для пользователя.");
            }
            var label_tag = new TagBuilder("label");
            label_tag.MergeAttribute("for", String.Format("{0}_{1}", name_prefix, name_element));
            label_tag.SetInnerText(_object["Name"].ToString());

            var input_tag = new TagBuilder("input");
            input_tag.MergeAttribute("data-val", "true");
            input_tag.MergeAttribute("id", String.Format("{0}_{1}", name_prefix, name_element));
            input_tag.MergeAttribute("name", String.Format("{0}.{1}", name_prefix, name_element));
            input_tag.MergeAttribute("type", "checkbox");
            input_tag.MergeAttribute("value", "true");

            var input_hidden_tag = new TagBuilder("input");
            input_hidden_tag.MergeAttribute("name", String.Format("{0}.{1}", name_prefix, name_element));
            input_hidden_tag.MergeAttribute("type", "hidden");
            input_hidden_tag.MergeAttribute("value", "true");

            var p_tag = new TagBuilder("p");
            p_tag.InnerHtml = label_tag.ToString(TagRenderMode.Normal) +
                              input_tag.ToString(TagRenderMode.SelfClosing) +
                              input_hidden_tag.ToString(TagRenderMode.SelfClosing);
            layout.Append(p_tag.ToString(TagRenderMode.Normal));

            return new HtmlString(layout.ToString());
        }
    }
}