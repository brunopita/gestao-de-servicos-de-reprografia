using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using System.IO;

namespace Reprografia.Data.XL
{
    public abstract class XlWriterStrategy
    {
        public HSSFWorkbook Book { get; set; }

        public void WriteAll()
        {
            foreach (var item in values)
                this.Write(item.Key, item.Value);
        }

        public void Write(string address, string value)
        {
            var cell = GetCell(address);
            cell.SetCellValue(value);
        }

        private NPOI.SS.UserModel.ICell GetCell(string rangeName)
        {
            var range = Book.GetNameAt(Book.GetNameIndex(rangeName));
            var areaRef = new AreaReference(range.RefersToFormula);

            CellReference[] crefs = areaRef.GetAllReferencedCells();

            CellReference cref = crefs[0];
            return Book.GetSheet(cref.SheetName)
                .GetRow(cref.Row)
                .GetCell(cref.Col);
        }

        //Deve ser definido em classe concreta
        protected Dictionary<string, string> values = new Dictionary<string, string>();

        protected void AddValue(string address, string value)
        {
            values.Add(address, value);
        }

        public abstract string DestinationFilename { get; }
        public abstract string ModelFilename { get; }
    }
}