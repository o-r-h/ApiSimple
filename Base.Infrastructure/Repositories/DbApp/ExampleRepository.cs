using Base.Domain.Entities.DbApp;
using Base.Domain.Interfaces.DbApp.RepositoryInterface;
using Base.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Infrastructure.Repositories.DbApp
{
    public class ExampleRepository :  IExampleRepository
    {
        private DbAppContext context;

        public ExampleRepository(DbAppContext context) 
        {
            this.context = context;
        }


        public async Task<long> Create(Example example)
        {
            context.Set<Example>().Add(example);
            await context.SaveChangesAsync();
            return example.IdExample;
        }

        public async Task Update(long idExample, Example example)
        {
            var exampleRecord = GetExample(idExample);
            context.Entry(exampleRecord).State = EntityState.Detached;
            context.Entry(example).State = EntityState.Modified;
            await context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Example>> GetAll()
        {
            
            return  await Task.Run(() =>  context.Set<Example>().ToListAsync() ); 
        }

        public async Task<IQueryable<Example>> GetPagination()
        {
        
            return await Task.Run(() => context.Set<Example>().AsQueryable().AsNoTracking());
        }

        public async Task Delete(long idExample)
        {
            context.Set<Example>().Remove(await GetExample(idExample));
            await context.SaveChangesAsync();
        }


        public async Task<Example> GetExample(long? idExample)
        {
          
                return await context.Set<Example>().FindAsync(idExample);
       
            
        }


    }
}
