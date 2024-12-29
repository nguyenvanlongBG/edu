using Bg.EduSocial.Constract.Cores;

namespace Bg.EduSocial.Application
{
    public class ContextService: IContextService
    {
        private ContextData _contextData;
        public ContextData GetContextData()
        {
            if (_contextData == null) return new ContextData();
            return _contextData;
        }

        public void SetContextData(ContextData contextData)
        {
            _contextData = contextData;
        }
    }

}
