using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Shared.Editor
{
    public enum EditorItemType
    {
        TEXT,
        FOMUALA,
        IMAGE,
        VIDEO,
        LINK,
        ATTRIBUTES  
    }
    public enum EditorActionType
    {
        NONE,
        INSERT,
        DELETE,
        FORMAT
    }
}
