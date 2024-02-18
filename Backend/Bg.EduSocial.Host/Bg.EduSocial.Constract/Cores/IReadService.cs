using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Cores
{
    public interface IReadService <TEntity, TEntityDto>
    {
        Task<TEntityDto> GetById(Guid id);
        Task<IEnumerable<TEntityDto>> FilterAsync();
    }
}
