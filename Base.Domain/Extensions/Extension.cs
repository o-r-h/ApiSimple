using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Extensions
{
    public static class Extension
    {
        public static bool ContainsIgnoreAll(this string text, string search)
        {
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(text, search, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) >= 0;
        }

        public static bool EqualsIgnoreAll(this string text, string secondText)
        {
            return CultureInfo.InvariantCulture.CompareInfo.Compare(text, secondText, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;
        }

        public static string RemoveTildes(this string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                ).Normalize(NormalizationForm.FormC);
        }

        public static string UppercaseWithoutTilde(this string s)
        {
            return s.RemoveTildes().ToUpper();
        }

        public static string[] Split(this string s, params string[] separators)
        {
            return s.Split(separators, StringSplitOptions.None);
        }

        public static bool IsInteger(this string s)
        {
            long number = 0;
            return long.TryParse(s, out number);
        }

        public static bool In(this string s, params string[] array) => array.Contains(s);

        public static bool In(this int n, params int[] array) => array.Contains(n);

        public static bool In(this int? n, params int[] array) => n.HasValue && array.Contains(n.Value);

        public static dynamic ToObjeto(this IDictionary<string, object> d)
        {
            var expandoObj = new ExpandoObject();
            var expandoObjCollection = (ICollection<KeyValuePair<String, Object>>)expandoObj;

            foreach (var keyValuePair in d)
            {
                expandoObjCollection.Add(keyValuePair);
            }

            return expandoObj as dynamic;
        }

        //Excel
        //public static Cell Add(this List<Cell> cells, int row, int columnIndex, string value, int columnWidth = -1, bool wrap = false)
        //{
        //    var cell = new Cell { Row = row, ColumnIndex = columnIndex, Value = value, WrapText = wrap };
        //    if (columnWidth != -1)
        //    {
        //        cell.ColumnWidth = columnWidth;
        //    }

        //    cells.Add(cell);
        //    return cell;
        //}

        //public static Entities.Excel.Range Add(this List<Entities.Excel.Range> ranges, int startRow, int startColumn, int endRow, int endColumn)
        //{
        //    var range = new Entities.Excel.Range { StartRow = startRow, StartColumnIndex = startColumn, EndRow = endRow, EndColumnIndex = endColumn };
        //    ranges.Add(range);
        //    return range;
        //}
    }
}
