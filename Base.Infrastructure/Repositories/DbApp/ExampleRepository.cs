using AutoMapper;
using Base.Domain.Entities.DbApp;
using Base.Domain.Interfaces.DbApp.RepositoryInterface;
using Base.Domain.Models.Example;
using Base.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Infrastructure.Repositories.DbApp
{
    public class ExampleRepository : IExampleRepository
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
            var exampleRecord = context.Set<Example>().Find(idExample);
            
            exampleRecord.NameExample = example.NameExample;
            exampleRecord.IdExample = idExample;
            exampleRecord.ModifiedBy = example.ModifiedBy;
            exampleRecord.ModifiedAt = System.DateTime.Now;
            exampleRecord.PriceExample = example.PriceExample;

            context.Entry(exampleRecord).State = EntityState.Modified;
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Example>> GetAll()
        {

            return await Task.Run(() => context.Set<Example>().ToListAsync());
        }

        public IQueryable<ExampleModel> GetAllIQueryable()
        {

            var query = from r in context.Examples select new ExampleModel {
             CreatedAt = r.CreatedAt,
             CreatedBy = r.CreatedBy,
           //  IdExample = r.IdExample,
             ModifiedAt = r.ModifiedAt,
             ModifiedBy = r.ModifiedBy,
             PriceExample = r.PriceExample,
             NameExample = r.NameExample
			};
            return query;
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
