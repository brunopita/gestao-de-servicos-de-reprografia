using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Reprografia.Data.XL
{
    public abstract class XlWriterStrategy
    {
        //Deve ser definido estaticamente em classe concreta
        public abstract Dictionary<string, string> Values
        {
            get;
        }

        public abstract string ModelFilename { get; }
    }
}