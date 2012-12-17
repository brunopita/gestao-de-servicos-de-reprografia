using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Excel;

namespace Reprografia.lib
{
    public class ExcelWrapper : IDisposable
    {
        private Application _App;
        public Application App
        {
            get
            {
                if (_App == null)
                    _App = new Application();
                return _App;
            }
        }

        public bool CloseApp()
        {
            if (_App != null)
            {
                _App.Quit();
                _App = null;
                return true;
            }
            return false;
        }


        private static void WritePropertyToSheet(Worksheet sheet, object source, System.Reflection.PropertyInfo prop, params string[] exclude)
        {
            Type propType = prop.PropertyType;
            if ((propType == typeof(string) || propType.IsPrimitive) && !exclude.Contains(prop.Name))
            {
                sheet.Range[prop.Name].Value = prop.GetValue(source, null);
            }
        }

        public void Dispose()
        {
            this.CloseApp();
        }
    }
}