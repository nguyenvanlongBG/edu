using Bg.EduSocial.Domain.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Base
{
    public class PagingParam
    {
        public int take { get; set; }

        public int skip { get; set; }
        public List<FilterCondition>? filters { get; set; }

    }
}
