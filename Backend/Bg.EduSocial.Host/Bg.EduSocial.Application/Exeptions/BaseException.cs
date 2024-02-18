using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application.Exeptions
{
    public class BaseException
    {
        public int ErrorCode { get; set; }
        public List<string> UserMessages { get; set; }
        public List<string> DevMessages { get; set; }
        // Mã truy xuất
        public string TraceId
        {
            get; set;
        }
        public string MoreInfo
        {
            get; set;
        }
        public BaseException(int errorCode, List<string> userMessages, List<string> devMessages, string traceId, string moreInfo)
        {
            ErrorCode = errorCode;
            UserMessages = userMessages;
            DevMessages = devMessages;
            TraceId = traceId;
            MoreInfo = moreInfo;
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
