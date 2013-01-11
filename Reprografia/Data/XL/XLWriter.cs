using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;


namespace Reprografia.Data.XL
{
    public class XLWriter : IDisposable
    {

        public XLWriter(string xlRootPath, XlWriterStrategy writer = null)
        {
            if (writer != null)
	            this.WriterStrategy = writer;
            //Definir caminho do arquivo de modelo
            this.ModelPath = Path.ChangeExtension(Path.Combine(xlRootPath, writer.ModelFilename),".xls");

            //Definir caminho do arquivo de destino
            this.CurrentPath = Path.Combine(xlRootPath, this.WriterStrategy.DestinationFilename);

            //Abrir arquivo para edicao como objeto NPOI
        }

        public string ModelPath { get; private set; }
        public string CurrentPath { get; private set; }
        public XlWriterStrategy WriterStrategy { get; private set; }

        public void Dispose()
        {

        }
    }
}