using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http
{
    public class Param
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class HeaderParam : Param { }
    public class QueryParam : Param { }
    public class BodyParam : Param { }
}
