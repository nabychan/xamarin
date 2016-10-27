using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http
{
    public enum HttpMethod : byte
    {
        NONE = 0,
        GET = 1,
        POST = 2,
        PUT = 3,
        DELETE = 4
    }
}
