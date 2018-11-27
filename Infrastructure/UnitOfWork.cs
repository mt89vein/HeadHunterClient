using System;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        public DbConnection Connection { get; }

        private bool Completed { get; set; }

        public ApplicationContext Context { get; }

        public DbTransaction Transaction { get; private set; }

        public UnitOfWork(ApplicationContext context)
        {
            Context = context;
            Connection = context.Database.GetDbConnection();
            Connection.Open();
            Completed = false;
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            Transaction = Connection.BeginTransaction(isolationLevel);
            Context.Database.UseTransaction(Transaction);
        }

        public void Complete()
        {
            Context.SaveChanges();
            Completed = true;
        }

        public void Close()
        {
            if (Completed)
            {
                Transaction.Commit();
            }
            else
            {
                Transaction?.Rollback();
            }
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                    Connection.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}