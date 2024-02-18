using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application.Exeptions
{
    public class NotFoundException : GeneralException
    {
        public NotFoundException(List<string> userMessages, List<string> devMessages) : base(404, userMessages, devMessages)
        {

        }
    }
}
