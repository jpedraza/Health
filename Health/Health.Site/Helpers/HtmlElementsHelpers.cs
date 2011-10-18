using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace Health.Site.Helpers
{
    public static class HtmlElementsHelpers
    {
        public static MvcHtmlString Table<TObject>(this HtmlHelper helper, IEnumerable<object> objects)
        {
            return Table(helper, objects, typeof (TObject));
        }

        public static MvcHtmlString Table(this HtmlHelper helper, IEnumerable<object> objects, Type objectType)
        {
            PropertyInfo[] propertyInfos = objectType.GetProperties();
            var stringBuilder = new StringBuilder();
            var tableTag = new TagBuilder("table");
            IList listObjects = objects.ToList();
            foreach (var o in listObjects)
            {
                var tableTrTag = new TagBuilder("tr");
                var trBulder = new StringBuilder();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    var tableTdTag = new TagBuilder("td");
                    PropertyInfo property = objectType.GetProperty(propertyInfo.Name);
                    object value = property.GetValue(o, null);
                    tableTdTag.InnerHtml = value == null ? "Не определено." : value.ToString();
                    trBulder.AppendLine(tableTdTag.ToString(TagRenderMode.Normal));
                }
                tableTrTag.InnerHtml = trBulder.ToString();
                stringBuilder.AppendLine(tableTrTag.ToString(TagRenderMode.Normal));
            }
            tableTag.InnerHtml = stringBuilder.ToString();
            return new MvcHtmlString(tableTag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString MonthGrid(this HtmlHelper helper, DateTime date)
        {
            int[,] grid = MonthGrid(date);
            var table = new TagBuilder("table");
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                var row = new TagBuilder("tr");
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    var cell = new TagBuilder("td");
                    cell.SetInnerText(grid[j, i] == 0 ? "" : grid[j, i].ToString());
                    row.InnerHtml += cell.ToString(TagRenderMode.Normal);
                }
                table.InnerHtml += row.ToString(TagRenderMode.Normal);
            }
            return new MvcHtmlString(table.ToString(TagRenderMode.Normal));
        }

        private static int[,] MonthGrid(DateTime date)
        {
            DateTime monthBegin = date.AddDays(-date.Day + 1);
            Calendar calendar = CultureInfo.CurrentCulture.Calendar;
            int daysInMonth = calendar.GetDaysInMonth(monthBegin.Year, monthBegin.Month);
            int weekCountInMonth = (daysInMonth + (int) monthBegin.DayOfWeek)%7 == 0
                                       ? (daysInMonth + (int) monthBegin.DayOfWeek)/7
                                       : (daysInMonth + (int) monthBegin.DayOfWeek)/7 + 1;
            var grid = new int[7, weekCountInMonth];
            for (int i = 0, d = 1, j = (int)monthBegin.DayOfWeek - 1; i < weekCountInMonth; i++, j = 0)
            {
                for (; j < 7; j++)
                {
                    grid[j, i] = d >= daysInMonth + 1 ? 0 : d++;
                }
            }
            return grid;
        }
    }
}