using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Cores
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        void BeginTransaction();
        void Dispose();
        void Commit();
        void Rollback();
        IDbContextTransaction GetTransaction();
        TContext GetContext();
        void SaveChange();
    }
}
