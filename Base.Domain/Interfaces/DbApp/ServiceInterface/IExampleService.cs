using Base.Domain.Entities.DbApp;
using Base.Domain.Models.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sieve.Models;


namespace Base.Domain.Interfaces.DbApp.ServiceInterface
{
    public interface IExampleService 
    {
        Task<Byte[]> DownloadFile();

        Task<long> Create(ExampleModel exampleModel);

        Task Update(long idExample, ExampleModel exampleModel);

        Task Delete(long idExample);

        Task<IEnumerable<ExampleModel>> GetAll();

        Task<IQueryable<Example>> GetPagination(SieveModel sieveModel);

    }
}
