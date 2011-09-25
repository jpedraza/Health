using System.Web;
using System.Web.Mvc;
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

    public class RenderingFactory
    {
        #region Implementation of IRenderingService

        /// <summary>
        /// Получить разметку для параметра.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <returns>Разметка.</returns>
        public HtmlString GetLayout(Parameter parameter)
        {
            /* Type renderer_type = ...
             * IRenderer renderer = Activator.CreateInstance(renderer_type, parameter.MetaData)).
             * if (renderer == null) {
             *  throw new Exception("Незарегестированный тип отрисовщика.");
             * }
             * return renderer.GetLayout();
            */
            //Type render_type = TypeCode.
            //IRenderer renderer = Activator.CreateInstance(render_type, parameter.MetaData);
            IRenderer renderer;
            return null;
        }

        #endregion
    }

#region OldVersionOfRendering
    //public abstract class BaseRenderer
    //{
    //    protected BaseRenderer(MetaData meta_data)
    //    {
    //        MetaData = meta_data;
    //    }

    //    protected MetaData MetaData { get; set; }
    //}

    //public class SelectRenderer : BaseRenderer, IRenderer
    //{
    //    #region Implementation of IRenderer

    //    public SelectRenderer(MetaData meta_data) : base(meta_data)
    //    {

    //    }

    //    #endregion

    //    #region Implementation of IRenderer

    //    /// <summary>
    //    /// Получить разметку.
    //    /// </summary>
    //    /// <returns>Разметка.</returns>
    //    public HtmlString GetLayout()
    //    {
    //        // Формируем разметку.
            
    //        return MvcHtmlString.Create("<select></select>");
    //    }

    //    #endregion
    //}

    ///// <summary>
    ///// Класс отрисовщика текстовых полей формы ввода пользователя
    ///// </summary>
    //public class SelectTextBoxRender : BaseRenderer, IRenderer
    //{
    //    /// <summary>
    //    /// Инициализирует класс отрисовщика текстовых полей
    //    /// </summary>
    //    /// <param name="meta_data">Мета-данные </param>
    //    /// <param name="prefix"></param>
    //    public SelectTextBoxRender(object object_for_rendering, string prefix
    //    {
    //    }

    //    public MvcHtmlString GetLayout()
    //    {
            
    //    }
    //}
#endregion

    
    #region AlternativeRendereres
    public abstract class BaseRender
    {
        /// <summary>
        /// Объект предназначенный для отрисовки
        /// </summary>
        protected object object_for_rendering {get; set;}
        /// <summary>
        /// Формат имени HTML-тега
        /// </summary>
        protected string name;
        /// <summary>
        /// Иницилизирует объекта класса отрисовщика
        /// </summary>
        /// <param name="object_for_rendering">Объект предметной области, предназначенный для отрисовки</param>
        /// <param name="name">Имя HTML-тегов</param>
        public BaseRender(object object_for_rendering, string name)
        {
            this.object_for_rendering = object_for_rendering;
            this.name = name;
        }
    }

    public sealed class SelectTextBoxRenderer : BaseRender, IRenderer
    {
        /// <summary>
        /// Иницилизирует объекта класса отрисовщика для TextBox
        /// </summary>
        /// <param name="object_for_rendering">Объект предметной области, предназначенный для отрисовки</param>
        /// <param name="prefix">Префикс, для создания имен HTML-тегов</param>
        public SelectTextBoxRenderer(object object_for_rendering, string name)
            : base(object_for_rendering, name)
        {
        }

        public HtmlString GetLayout()
        {
            //Преобразуем
            var _object = object_for_rendering as Dictionary<string, string>;
            var LayoutString = new StringBuilder();
            if (_object != null)
            {
                //Отрисовываем название для пользователя
                var label_tag = new TagBuilder("label");
                label_tag.MergeAttribute("for", name);
                label_tag.SetInnerText(_object["Name"]);

                //Отрисовываем поле ввода текста.
                var input_tag = new TagBuilder("input");
                input_tag.MergeAttribute("id", name);
                input_tag.MergeAttribute("name", name);
                input_tag.MergeAttribute("value", _object["Value"]);

                //Оборачиваем в абзац.
                var p_tag = new TagBuilder("p")
                {
                    InnerHtml = label_tag.ToString(TagRenderMode.Normal) +
                                input_tag.ToString(TagRenderMode.SelfClosing)
                };
                LayoutString.Append(p_tag);
            }
            else
            {
                throw new Exception(String.Format("Невозможно отрисовать объект {0}",name));
            }
            return new HtmlString(LayoutString.ToString());
        }
    }

    /// <summary>
    /// Класс для отрисовки TextArea
    /// </summary>
    public sealed class SelectTextAreaRenderer : BaseRender, IRenderer
    {
        public SelectTextAreaRenderer(object object_for_rendering, string name)
            : base(object_for_rendering, name)
        {
        }

        public HtmlString GetLayout()
        {
            //Преобразуем
            var _object = object_for_rendering as Dictionary<string, string>;
            var LayoutString = new StringBuilder();
            if (_object != null)
            {
                //Отрисовываем название для пользователя
                var label_tag = new TagBuilder("label");
                label_tag.MergeAttribute("for", name);
                label_tag.SetInnerText(_object["Name"]);

                //Отрисовываем поле ввода текста.
                var textarea_tag = new TagBuilder("textarea");
                textarea_tag.MergeAttribute("name", name);
                textarea_tag.MergeAttribute("id", name);
                textarea_tag.SetInnerText(_object["Value"]);

                //Оборачиваем в абзац.
                var p_tag = new TagBuilder("p")
                {
                    InnerHtml = label_tag.ToString(TagRenderMode.Normal) +
                                textarea_tag.ToString(TagRenderMode.SelfClosing)
                };
                LayoutString.Append(p_tag);
            }
            else
            {
                throw new Exception(String.Format("Невозможно отрисовать объект {0}", name));
            }
            return new HtmlString(LayoutString.ToString());
        }
    }

    public sealed class SelectListBoxRender : BaseRender, IRenderer
    {
        public SelectListBoxRender(object object_for_rendering, string name)
            : base(object_for_rendering, name)
        {
        }

        public HtmlString GetLayout()
        {
            var _object = object_for_rendering as Dictionary<string, object>;
            var LayoutString = new StringBuilder();
            if (_object != null)
            {
                //Отрисовываем название для пользователя и обращение к нему
                var p_tag = new TagBuilder("p");
                p_tag.SetInnerText(String.Format("Заполните пожалуста параметр {0}. Для этого выберите один из перечисленых ниже вариантов", 
                    _object["Name"]));

                //Отрисовываем необходимые варианты ответа.
                if (_object["Variants"] != null)
                {
                    var Variants = _object["Variants"] as Dictionary<string, string>;
                    if (Variants == null)
                        throw new Exception(String.Format("Отсутствуют варианты для отрисовки объекта {0}", name));

                    //Начинаем отрисовку List Box
                    var container = new StringBuilder();
                    var select_tag = new TagBuilder("select");
                    select_tag.MergeAttribute("id", name);
                    select_tag.MergeAttribute("name", name);
                    select_tag.MergeAttribute("size", Variants.Count.ToString());
                    select_tag.MergeAttribute("multiple", "multiple");

                    foreach (var variant in Variants)
                    {
                        //Отрисовываем вариант.
                        var option_tag = new TagBuilder("option");
                        option_tag.SetInnerText(variant.Value);
                        option_tag.MergeAttribute("id", String.Format("{0}-{1}", name, variant.Key));
                        option_tag.MergeAttribute("name", String.Format("{0}-{1}", name, variant.Key));

                        //Собираем варианты ответов в один контейнер.
                        container.Append(option_tag);
                    }

                    //Добавляем контейнер с ответами в ListBox
                    select_tag.SetInnerText(container.ToString());

                    //Собираем все в абзац
                    var p_tag_common = new TagBuilder("p")
                    {
                        InnerHtml = p_tag.ToString(TagRenderMode.Normal) +
                                    select_tag.ToString(TagRenderMode.Normal)
                    };
                }
                else
                    throw new Exception(String.Format("Отсутствуют варианты для отрисовки объекта {0}", name));
            }
            else
            {
                throw new Exception(String.Format("Невозможно отрисовать объект {0}", name));
            }
            return new HtmlString(LayoutString.ToString());
        }
    }

    public sealed class SelectDropBoxRender : BaseRender, IRenderer
    {
        public SelectDropBoxRender(object object_for_rendering, string name)
            : base(object_for_rendering, name)
        {
        }

        public HtmlString GetLayout()
        {
            var _object = object_for_rendering as Dictionary<string, object>;
            var LayoutString = new StringBuilder();
            if (_object != null)
            {
                //Отрисовываем название для пользователя и обращение к нему
                var p_tag = new TagBuilder("p");
                p_tag.SetInnerText(String.Format("Заполните пожалуста параметр {0}. Для этого выберите один из перечисленых ниже вариантов", 
                    _object["Name"]));

                //Отрисовываем необходимые варианты ответа.
                if (_object["Variants"] != null)
                {
                    var Variants = _object["Variants"] as Dictionary<string, string>;
                    if (Variants == null)
                        throw new Exception(String.Format("Отсутствуют варианты для отрисовки объекта {0}", name));

                    //Начинаем отрисовку List Box
                    var container = new StringBuilder();
                    var select_tag = new TagBuilder("select");
                    select_tag.MergeAttribute("id", name);
                    select_tag.MergeAttribute("name", name);
                    //select_tag.MergeAttribute("size", Variants.Count.ToString());
                    //select_tag.MergeAttribute("multiple", "multiple");

                    foreach (var variant in Variants)
                    {
                        //Отрисовываем вариант.
                        var option_tag = new TagBuilder("option");
                        option_tag.SetInnerText(variant.Value);
                        option_tag.MergeAttribute("id", String.Format("{0}-{1}", name, variant.Key));
                        option_tag.MergeAttribute("name", String.Format("{0}-{1}", name, variant.Key));

                        //Собираем варианты ответов в один контейнер.
                        container.Append(option_tag);
                    }

                    //Добавляем контейнер с ответами в ListBox
                    select_tag.SetInnerText(container.ToString());

                    //Собираем все в абзац
                    var p_tag_common = new TagBuilder("p")
                    {
                        InnerHtml = p_tag.ToString(TagRenderMode.Normal) +
                                    select_tag.ToString(TagRenderMode.Normal)
                    };
                }
                else
                    throw new Exception(String.Format("Отсутствуют варианты для отрисовки объекта {0}", name));
            }
            else
            {
                throw new Exception(String.Format("Невозможно отрисовать объект {0}", name));
            }
            return new HtmlString(LayoutString.ToString());
        }
    }
    #endregion
}