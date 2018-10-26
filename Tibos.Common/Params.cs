using System;
using System.Collections.Generic;
using System.Text;

namespace Tibos.Common
{
    [Serializable]
    public class Params
    {
        public string key { get; set; }

        public string values { get; set; }
    }

    [Serializable]
    public class Paging
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }

    [Serializable]
    public class Sort
    {
        public string key { get; set; }

        public Common.EnumBase.OrderType searchType { get; set; }
    }
}
