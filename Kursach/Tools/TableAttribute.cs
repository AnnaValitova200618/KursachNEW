using System;
using System.Collections.Generic;
using System.Text;

namespace Kursach.Tools
{
    internal class TableAttribute : Attribute
    /*генерация запросов */
    {
        public string Table { get; }
        public TableAttribute(string table) 
        {
            Table = table;
        }
    }
}
