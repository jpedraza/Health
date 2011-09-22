using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Health.Site.Helpers
{
    public static class HtmlElementsHelpers
    {
        public static MvcHtmlString Table<TObject>(this HtmlHelper helper, IEnumerable<object> objects)
        {
            return Table(helper, objects, typeof (TObject));
        }

        public static MvcHtmlString Table(this HtmlHelper helper, IEnumerable<object> objects, Type object_type)
        {
            PropertyInfo[] property_infos = object_type.GetProperties();
            var string_builder = new StringBuilder();
            var table_tag = new TagBuilder("table");
            IList list_objects = objects.ToList();
            foreach (var o in list_objects)
            {
                var table_tr_tag = new TagBuilder("tr");
                var tr_bulder = new StringBuilder();
                foreach (PropertyInfo property_info in property_infos)
                {
                    var table_td_tag = new TagBuilder("td");
                    PropertyInfo property = object_type.GetProperty(property_info.Name);
                    object value = property.GetValue(o, null);
                    table_td_tag.InnerHtml = value == null ? "Не определено." : value.ToString();
                    tr_bulder.AppendLine(table_td_tag.ToString(TagRenderMode.Normal));
                }
                table_tr_tag.InnerHtml = tr_bulder.ToString();
                string_builder.AppendLine(table_tr_tag.ToString(TagRenderMode.Normal));
            }
            table_tag.InnerHtml = string_builder.ToString();
            return new MvcHtmlString(table_tag.ToString(TagRenderMode.Normal));
        }
    }
}