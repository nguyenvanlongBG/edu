using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application.Exeptions
{
    public class InternalException : GeneralException
    {
        public InternalException(List<string> userMessages, List<string> devMessages) : base(500, userMessages, devMessages)
        {
        }
    }
}
