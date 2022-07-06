using Base.Domain.Entities.DbApp;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Base.Domain.Interfaces.DbApp.RepositoryInterface
{
    public interface IExampleRepository 
    {


        Task<long> Create(Example example);

        Task Update(long idExample, Example example);

        Task<IEnumerable<Example>> GetAll();

        Task Delete(long idExample);

        Task<Example> GetExample(long? idExample);

        Task<IQueryable<Example>> GetPagination();

    }

    
}