using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Cores
{
    public interface IContextService
    {
        ContextData GetContextData();
        void SetContextData(ContextData contextData);
    }
}
