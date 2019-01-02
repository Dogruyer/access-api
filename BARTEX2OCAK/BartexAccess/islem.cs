using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

namespace BartexAccess
{
    public class islem
    {
        public static void LogEkle(DataTable table)
        {
            XmlDocument document = new XmlDocument();
            document.Load(HttpContext.Current.Server.MapPath("~/kartno.xml"));
            XmlNode root = document.SelectSingleNode("/root");
            root.RemoveAll();
            XmlDocumentFragment xdf = document.CreateDocumentFragment();

            foreach (System.Data.DataRow row in table.Rows)
            {
                foreach (System.Data.DataColumn column in table.Columns)
                {
                    var colunmname = column.ColumnName.Replace(" ", "");
                    var columnTR = String.Join("", column.ColumnName.Normalize(System.Text.NormalizationForm.FormD).Where(x => char.GetUnicodeCategory(x) != System.Globalization.UnicodeCategory.NonSpacingMark));
                    xdf.InnerXml = "<item><" + colunmname + ">" + row[column].ToString() + "</" + colunmname + "></item>";
                    root.InsertAfter(xdf, root.LastChild);
                }
            }
            document.Save(HttpContext.Current.Server.MapPath("~/kartno.xml"));
        }
    }
}