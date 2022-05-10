using System;
using System.Collections.Generic;
using System.Text;

namespace Kursach.Tools
{
    internal class ColumnAttribute : Attribute
        /*генерация запросов */
    {
        public string Column { get; }
        public ColumnAttribute(string column)
        {
            Column = column;
        }
    }
}
