using Bg.EduSocial.Constract.Cores;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bg.EduSocial.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bg.EduSocial.EFCore.Repositories
{
    public class UnitOfWork<TContext>: IUnitOfWork<TContext>, IDisposable where TContext : DbContext
    {
        private bool _disposed;
        private IDbContextTransaction _transaction;

        public TContext _context { get; }

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }
        public void Save()
        {
                //Calling DbContext Class SaveChanges method 
                _context.SaveChanges();
        }
        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }

        public IDbContextTransaction GetTransaction()
        {
            if (_transaction is null)
            {
                _transaction = _context.Database.BeginTransaction();
            }
            return _transaction;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
        public void DisposeTransaction()
        {
            _transaction.Dispose();
        }

        public TContext GetContext()
        {
            return _context;
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }
    }
}
