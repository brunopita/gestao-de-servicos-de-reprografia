using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Reprografia.Models
{
    [TypeConverter(typeof(AvaliacaoNotaEnumConverter))]
    public enum AvaliacaoNotaEnum
    {
        A = 'A',

        X = 'X',

        NA = 'N'
    }
}
