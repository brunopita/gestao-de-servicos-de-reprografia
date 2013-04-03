using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Util;

namespace Reprografia.Data.XL
{
    public class XLWriter
    {
        public XLWriter(string xlRootPath, XlWriterStrategy writer = null)
        {
            if (writer != null)
                this.WriterStrategy = writer;
            //Definir caminho do arquivo de modelo
            this.ModelPath = Path.Combine(xlRootPath, writer.ModelFilename);

            //Abrir arquivo para edicao como objeto NPOI
            this.Book = new HSSFWorkbook(new FileStream(this.ModelPath, FileMode.Open, FileAccess.Read));
        }

        public HSSFWorkbook Book { get; set; }

        public virtual void WriteAll()
        {
            foreach (var item in this.WriterStrategy.Values)
                this.Write(item.Key, item.Value);

        }

        public virtual void WriteAll(Stream stream)
        {
            this.WriteAll();
            Book.Write(stream);
        }

        public virtual void Write(string address, string value)
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

        public string ModelPath { get; private set; }
        public string CurrentPath { get; private set; }
        public XlWriterStrategy WriterStrategy { get; private set; }
    }
}