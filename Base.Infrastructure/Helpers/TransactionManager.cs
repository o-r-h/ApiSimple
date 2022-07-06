using Base.Domain.Entities;
using Base.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Base.Infrastructure.Helpers
{
    public class TransactionManager<T> where T : class
    {
        public const int INSERT = 1;
        public const int UPDATE = 2;
        public const int DELETE = 3;

        private DbContext context;
        private DbSet<T> dbSet;

        public TransactionManager(DbContext context, DbSet<T> _dbSet)
        {
            this.context = context;
            dbSet = _dbSet;
        }

        public Task<T> Add(T data)
        {
            return Save(data, INSERT, null);
        }

        public Task<T> Add(T data, Func<T, Task> additionalTasks)
        {
            return Save(data, INSERT, (d, action) => additionalTasks(d));
        }

        public Task<T> Update(T data)
        {
            return Save(data, UPDATE, null);
        }

        public Task<T> Update(T data, Func<T, Task> additionalTasks)
        {
            return Save(data, UPDATE, (d, action) => additionalTasks(d));
        }

        public Task<T> Delete(T data)
        {
            return Save(data, DELETE, null);
        }

        public Task<T> Delete(T data, Func<T, Task> additionalTasks)
        {
            return Save(data, DELETE, (d, action) => additionalTasks(d));
        }

        public void SetCommonValues(T data, int action)
        {
            // if (action == DELETE)
            // {
            //     data.State = INACTIVE_STATE;
            // }
        }

        public async Task Execute(T data, int action)
        {
            if (action == INSERT)
            {
                await dbSet.AddAsync(data);
            }
            else
            {
                context.Entry(data).State = EntityState.Modified;
                // if (action == UPDATE)
                // {
                //     context.Entry(data).Property(x => x.State).IsModified = false;
                // }
            }
        }

        public async Task<R> Transaccion<R>(Func<Task<R>> transactionTasks)
        {
            using (var transaccion = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var data = await transactionTasks();
                    context.SaveChanges();
                    transaccion.Commit();
                    return data;
                }
                catch (Exception e)
                {
                    transaccion.Rollback();
                    throw e;
                }
            }
        }

        public Task<T> Save(T data, int action, Func<T, int, Task> additionalTasks) =>
            Transaccion(async () =>
            {
                SetCommonValues(data, action);
                if (additionalTasks != null)
                {
                    await additionalTasks(data, action);
                }
                await Execute(data, action);
                return data;
            });
    }
}
