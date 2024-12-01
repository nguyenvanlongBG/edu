using Bg.EduSocial.Constract.Questions;
using Bg.EduSocial.Constract;
using Bg.EduSocial.Domain.Questions;
using Bg.EduSocial.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Application
{
    public class OptionService : WriteService<IOptionRepo, OptionEntity, OptionDto, OptionEditDto>, IOptionService
    {
        public OptionService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
