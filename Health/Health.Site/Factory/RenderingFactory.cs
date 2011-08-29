using System.Web;
using System.Web.Mvc;
using Health.Core.Entities;
using Health.Core.Entities.POCO;

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
            return null;
        }

        #endregion
    }

    public abstract class BaseRenderer
    {
        protected BaseRenderer(MetaData meta_data)
        {
            MetaData = meta_data;
        }

        protected MetaData MetaData { get; set; }
    }

    public class SelectRenderer : BaseRenderer, IRenderer
    {
        #region Implementation of IRenderer

        public SelectRenderer(MetaData meta_data) : base(meta_data)
        {

        }

        #endregion

        #region Implementation of IRenderer

        /// <summary>
        /// Получить разметку.
        /// </summary>
        /// <returns>Разметка.</returns>
        public HtmlString GetLayout()
        {
            // Формируем разметку.
            return MvcHtmlString.Create("<select></select>");
        }

        #endregion
    }
}