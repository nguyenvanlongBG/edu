using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application.Exeptions
{
    public class GeneralException : Exception
    {
        public int ErrorCode
        {
            get; set;
        }
        public List<string> UserMessages
        {
            get; set;
        }
        public List<string> DevMessages
        {
            get; set;
        }
        public GeneralException(int errorCode, List<string> userMessages, List<string> devMessages)
        {
            ErrorCode = errorCode;
            UserMessages = userMessages;
            DevMessages = devMessages;
        }
    }
}
